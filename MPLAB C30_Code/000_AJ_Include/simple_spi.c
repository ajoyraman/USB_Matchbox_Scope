// Aj SPI function
// contains the delay function also
// Uses PortE 0-4 Pins
// 25 July 2013

//----------------------------------
//SPI  USAGE
//setpga(gain=xx,pga=yy); 
//xx=0 for Gain1, 1 for Gain2, 2 for Gain 4, 3 for Gain 5
//yy=0 for PGA1, 1 for PGA2, 
//------------------------------------
//DELAY USAGE
//delay(zz);
//zz= 1 to 65536
//zz=50 gives 25 microSec
//------------------------------------
#include <p30f2020.h>
#include <libpic30.h>
                    
#define spic   LATEbits.LATE0  //  SPI Clock
#define spid   LATEbits.LATE1  //  SPI Data
#define cs1    LATEbits.LATE2  //  CS Bar for PGA1
#define cs2    LATEbits.LATE3  //  CS bar for PGA2


//unsigned long x;
int gain,pga;//

//void delay(unsigned long dog);
void setpga(int gain, int pga); //gain=gain setting, pga= 1 or 2 

/*
//-------------------------------------------------------------
void delay(unsigned long dog){
 unsigned long x;	
 for( x=0; x<dog; x++){;} //Delay apx 20ms	
}	
*/
//-------------------------------------------------------------
void setpga(int gain,int pga){

//TRISE=0X00; //all outputs
//Selecting only those used by this function

TRISEbits.TRISE0=0; //SPI Clock out
TRISEbits.TRISE1=0; //SPI Data out
TRISEbits.TRISE2=0; //CS bar for PGA1
TRISEbits.TRISE3=0; //CS bar for PGA2


// spic,spid,cs1,cs2
int count; 
int testvector=0X80; 
//initial values
cs1=1;cs2=1;spic=0;
//start the instruction cycle

if (pga==1)
	cs1=0;
else
	cs2=0;

for (count=0; count<=7;count++){
	if (count==1){
	spid=1;
	__delay32(320); //0.1usec
	spic=1;
	__delay32(320);
	spic=0;
	}
	else {
	spid=0;
	__delay32(320);
	spic=1;
	__delay32(320);
	spic=0;
	}
}
//continue the gain bits
for (count=0; count<=7;count++){
	if((gain & testvector) >0 ){
    spid =1;
	__delay32(320);
	spic=1;
	__delay32(320);
	spic=0;
	}
	else{
	spid =0;
	__delay32(320);
	spic=1;
	__delay32(320);
	spic=0;
	}
    testvector=testvector>>1;
}
__delay32(320);

if (pga==1)
	cs1=1;
else
	cs2=1;
}

