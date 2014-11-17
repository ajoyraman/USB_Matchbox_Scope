//Initialize the ADC Channels 
//Initialise the COMP3A and COMP3B 
//Ajoy 2 Aug 13
//
//----------------------------------------------------------------------------------
//  initialiseADC () Sets up AN0-AN3
/*  Sample Calling Routine for AN0/AN2
	int temp;
	char AN[405];

    ADCPC0bits.SWTRG0 = 1; //start conversion of AN3 and AN2
    while(ADCPC0bits.PEND0){} //conv pending becomes 0 when conv complete
       	AN[temp]=ADCBUF0>>2;
        temp++;
		AN[temp]=ADCBUF1>>2;
		temp++;
		*/
		
//----------------------------------------------------------------------------------

#include <p30f2020.h>

// Function prototypes
void __attribute__((__interrupt__, __auto_psv__)) _CMP3Interrupt(void);
void initialiseADC(void);
void initialiseComp(void);


void initialiseADC (void){
//This function initializes the ADC for a dsPIC30F2020
    // ADCON: ADC Control Register 1
     //ADCONbits.ADCS      = 4;     //100 = FADC/12 = 20.0 MHz @ 30 MIPS
     								// Fadc=Xtal*16 Divider is set up for Fadc/12 
									// 16 MHz with 12MHz Xtal
	 ADCONbits.ADCS       = 2;     //010 = FADC/8 = 30.0 MHz @ 30 MIPS
	 								
	 //ADCONbits.ADCS        = 3;  	//011 = FADC/10 = 24.0 MHz @ 30 MIPS								
									
     ADCONbits.SEQSAMP   = 0;       // Simultaneous Sampling 
     ADCONbits.ORDER     = 0;       // Even channel first 
     //ADCONbits.EIE       = 0;       // Interrupt after second conversion 
     ADCONbits.FORM      = 0;       // Output in Integer Format   
     ADCONbits.GSWTRG    = 0;       // global software trigger bit
     ADCONbits.ADSIDL    = 0;       // Operate in Idle Mode 
 
     ADPCFG = 0b1111111111000000;    // AN0-AN5 all analog inputs
	 // TRIS ?	
 
     ADSTAT = 0;     // Clear the ADSTAT register 
     
     //Settings for AN3 AN2
     ADCPC0bits.TRGSRC1 = 1;   // source is individual software trigger for AN3 AN2
     ADCPC0bits.SWTRG1 = 0;    // When set to 1 will  start conversion of AN3 and AN2
     ADCPC0bits.PEND1 = 0;     // goes to 1 on conversion pending & 0 on  conversion complete     ADCPC0bits.IRQEN1 = 0;    // disable the interrupt
     ADCPC0bits.IRQEN1 = 0;    // interrupt is not generated
	 
	 //Settings for AN1 AN0
	 ADCPC0bits.TRGSRC0 = 1;   // source is individual software trigger for AN1 AN0
     ADCPC0bits.SWTRG0 = 0;    // When set to 1 will  start conversion of AN1 and AN0
     ADCPC0bits.PEND0 = 0;     // goes to 1 on conversion pending & 0 on  conversion complete     ADCPC0bits.IRQEN0 = 0;    // disable the interrupt
     ADCPC0bits.IRQEN0 = 0;    // interrupt is not generated at end of conversion
 
     ADCONbits.ADON = 1; //Start the ADC module
 
	
}
void initialiseComp(void){
	
	CMPCON3bits.CMPSIDL=1; //Continue in IDLE
	CMPCON3bits.EXTREF=0; //Internal Reference
	//CMPCON3bits.CMPPOL=0; //Normal input polarity ----------Set in Main
	CMPCON3bits.RANGE =1; //High Range VDD/2
	// CMPCON3bits.INSEL=0; // 3A selected ----------Set in Main
	//CMPCON3bits.CMPSTAT=0; //Status including CMPPOL
	
	//CMPDAC3bits.CMREF= 512; //2.5V/2 initial setting ----------Set in Main
	
	//CMPCON3bits.CMPON=1; //Comparator ON ----------Set in Main
	
		
	
	
	//Interrupt priority and enable
	IPC7bits.AC3IP =3; //Priority for AC3 Interrupt
	
	IFS1bits.AC3IF =0;  //Analog Comparator #3 Interrupt Flag Status bit
	
	// IEC1bits.AC3IE =0; // Set 1 to Enable interrupt ----------Set in Main
	
}

