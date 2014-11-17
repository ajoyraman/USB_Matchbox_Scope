// AjScope2 with 3byteCommands with "Reset"
// 02 Aug 2013: Basic Command Decode Matrix
//				Read VRef AN2 AN3 and return result as 4 bytes
//				Set Offset OC1 & OC2 PWM
// 03 Aug 2013: Added the Auto Mode Capture with Sample rate Settings
// 				Added the LED Test Mode
// 
// ------------------------------------------------------------
// 
// 
// 
//-------------------------------------------------------------
#include <p30f2020.h>
#include <rs232_C30.c>
#include <libpic30.h>
#include <adc_comparator.c>
#include <OCxPWM.c>
//#include <simple_spi.c>

//Fuzes 16 MHz crystal
_FOSC(CSW_FSCM_OFF & OSC2_IO & HS);//No Clock Switching,OSC2 is an output, HS Crystal
_FOSCSEL(PRIOSC_PLL); 	// Primary Osc With PLL FPWM= Primary Osc X 32 ,
						//FOSC = Primary Osc X 4 FADC = Primary Osc X 16 ;
						//FCY = Primary Osc X 2 
_FWDT(FWDTEN_OFF);                            //Turn off WatchDog Timer
_FGS(CODE_PROT_OFF);                          //Turn off code protect
_FPOR( PWRT_OFF );                            //Turn off power up timer




//Function Prototypes
void readVref(void);
void capture_data(void);
void set_sampling(unsigned char rate);	

unsigned char input_string[3],AN[405],data;
char capture_mode=0,error=0;
int data_index, adc_index;
unsigned long delay_cycles=10, sliding_delay_cycles;


#define busy   LATEbits.LATE4 //  Busy


//----------------------------------------------------------------------
//Main Program	
int main( ){
		__delay32(3200);//initial delay 100uSec
		initUart(); //Initialise the UART
		__delay32(3200);//initial delay 100uSec
	    
		//TRISA=0; //PortA all inputs
		TRISE=0; //PortE all outputs
	    initialiseADC ();
		setupOC1PWM();
		setupOC2PWM();
	
		busy=1;
		
		//AjScope
		putHex(65);putHex(106);putHex(83);putHex(99);putHex(111);putHex(112);putHex(101);
		
	    
		
 	while(1){	
   
		for (data_index=1;data_index<=3;++data_index)
			input_string[data_index-1]= getHex();
		
			if (input_string[0]==65){ // "A"  ABORT
				goto restart;//return(0);
			}	
			else; 
			
			//putHex(input_string[0]);
			//putHex(input_string[1]);
			//putHex(input_string[2]);
	
			
			
		if ( error==0) { 

            if (input_string[0]==73){ 		//"I" Identify
				putHex(65);putHex(106);putHex(83);putHex(99);putHex(111);putHex(112);putHex(101);
            }
            else if (input_string[0]==65)  	//"A" Abort
				putHex(65);
            else if  (input_string[0]==82){  //"R" Read Vref
				putHex(82);
				readVref();			
			}
			else if  (input_string[0]==68){  //"D" Send Data
				putHex(68);
				for(adc_index=0;adc_index<400;adc_index++){
					putHex(AN[adc_index]);//8 bit AN0 Value
					adc_index++;
				}
			}		
			else if (input_string[0]==83){  	//"S" Sample Rate
				putHex(83);
				set_sampling(input_string[1]);
				}
            else if  (input_string[0]==78)  //"N" Noise Filter
				putHex(78);
			else if  (input_string[0]==67){  //"C" Capture Data
				putHex(67);
				capture_data();
			}	
			else if (input_string[0]==76){  //"L" Trig Level
				putHex(76);
				CMPDAC3bits.CMREF=input_string[1]*256 + input_string[2];//Comp3ref
			}
            else if  (input_string[0]==84){  //"T" Trig Source
				putHex(84);
				capture_mode=input_string[1]; // 0/1/2 auto/Ch1/Ch2
			}
			else if  (input_string[0]==80){  //"P" Trig Polarity
				putHex(80);
				CMPCON3bits.CMPPOL=input_string[1]; // 0/1 =Normal /Inverted polarity
			}
			else if  (input_string[0]==71)  //"G" Gains
				putHex(71);
			else if (input_string[0]==79){  	//"O" Offset Ch1
				putHex(79);
				OC1RS=input_string[1]*256 + input_string[2];//Set PWM1
   			}         
            else if  (input_string[0]==111){  //"o" Offset Ch2
				OC2RS=input_string[1]*256 + input_string[2];//Set PWM2
				putHex(111);
            }
			else if  (input_string[0]==70)  //"F" FFT mode
				putHex(70);
			else if  (input_string[0]==100)  //"d" Delay Post Trigger
				putHex(100);
			else if  (input_string[0]==116){  //"t" Test LED
				putHex(116);
				busy = ~ busy;
				}
			else { //"E" Error
				putHex(69);
				goto restart; 
   				}    		
		}
		
		
		//loop
	}
			
	
restart:
return(0);	
}
void readVref(void){
	//Reads the value of AN2 and AN3 and returns the 10bit Value
	// AN2msb);putHex(AN2lsb);putHex(AN3msb);putHex(AN3lsb);
	unsigned char AN2msb,AN2lsb,AN3msb,AN3lsb; 
	
    ADCPC0bits.SWTRG1 = 1; //start conversion of AN3 and AN2
	Nop();Nop();Nop();
    while(ADCPC0bits.PEND1){} //conv pending becomes 0 when conv complete
       	AN2lsb = ADCBUF2 &0x00ff;       // lsb of the ADC result
        AN2msb=(ADCBUF2 &0xff00)>>8;  // msb of the ADC result
		AN3lsb = ADCBUF3 &0x00ff;       // lsb of the ADC result
        AN3msb=(ADCBUF3 &0xff00)>>8;  // msb of the ADC result
		putHex(AN2msb);putHex(AN2lsb);putHex(AN3msb);putHex(AN3lsb);
}
void capture_data(void){
	busy=1;
	if (capture_mode==0) {	
		int temp=0;	
		ADCPC0bits.SWTRG0 = 1; //start conversion of AN3 and AN2
		while(temp<400){    
        
			while(ADCPC0bits.PEND0){} //conv pending becomes 0 when conv complete
				AN[temp]=ADCBUF0>>2;
				temp++;
				AN[temp]=ADCBUF1>>2;
				temp++;
				__delay32(delay_cycles);
			
				ADCPC0bits.SWTRG0 = 1; //start conversion of AN3 and AN2
		
        }
	}

	busy=0;
}
void set_sampling(unsigned char rate){
//	11 for rounding off to 2usec +64*N  for additional 2*N usec
	if(rate == 1)		// 2 us/sample 		500kbps 	20us/div
		delay_cycles=11;
	else if(rate == 2)  // 5 us/sample 		200kbps 	50us/div
		delay_cycles=11+96;
	else if(rate == 3)  // 10 us/sample 	100kbps  	0.1ms/div
		delay_cycles=11+256;
	else if(rate == 4)  // 20 us/sample 	50kbps  	0.2ms/div
		delay_cycles=11+576;
	else if(rate == 5)  // 50 us/sample 	20kbps  	0.5ms/div
		delay_cycles=11+1536;
	else if(rate == 6)  // 100 us/sample 	10kbps 		1ms/div
		delay_cycles=11+3136;
	else if(rate == 7)  // 200 us/sample 	5kbps 		2ms/div
		delay_cycles=11+6336;
	else if(rate == 8)  // 500 us/sample 	2kbps 		5ms/div
		delay_cycles=11+15936;
	else if(rate == 9)  // 1000 us/sample 	1kbps 		10ms/div
		delay_cycles=11+31936;
	else if(rate == 10)  // 2000 us/sample 	500bps 		20ms/div
		delay_cycles=11+63936;
	else if(rate == 11)  // 5000 us/sample 	200ps 		50ms/div
		delay_cycles=11+159936;
	else if(rate == 12)  // 10000 us/sample 100bps 		0.1s/div
		delay_cycles=11+319936;
	else if(rate == 13)  // 20000 us/sample 50bps 		0.2s/div
		delay_cycles=11+639936;
	else if(rate == 14)  // 50000 us/sample  20ps 		0.5s/div
		delay_cycles=11+1599936;
	else if(rate == 15)  // 100000 us/sample 10bps 		1.0s/div
		delay_cycles=11+3199936;



		
}




