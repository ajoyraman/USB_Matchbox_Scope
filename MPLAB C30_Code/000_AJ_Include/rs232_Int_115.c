
//UART Initialize and rs232 routines 115200 baud
//Ajoy 22 July 2013
//Modified 31 July 13 for 16 MHz Crystal
//Modified 04 Aug 2013 for Tx in Interrupt Mode
//----------------------------------------------------------------------------------
//  initUART(); sets up the UART RX in Interrupt mode 
//  Tx in interrupt mode
//  getHex(); Reads one byte from RX
//  putHex(); Writes one byte to TX  
//  clearRXBuffer(); does not work ?
//----------------------------------------------------------------------------------

#include <p30f2020.h>

// Function prototypes
void __attribute__((__interrupt__, __auto_psv__)) _U1RXInterrupt(void);
void __attribute__((__interrupt__, __auto_psv__)) _U1TXInterrupt(void);
void initUart(void);
void clearRXBuffer(void);
int getHex(void);
void putHex(int x); 

//global 
int RXflag=0, TXflag=0;

#define hw_reset   LATEbits.LATE5 //  HW_Reset

void initUart(void){
	TRISE=0;
	hw_reset =0;

	U1MODEbits.UARTEN=  0;    //UART1 Disabled
	//U1MODEbits.UARTEN=  1;    //UART1 Enabled
	U1MODEbits.ALTIO =  1;   // Use alternate IO
	U1MODEbits.BRGH =   0;    //Use High Bit rate
	//U1MODEbits.BRGH =   1;    //Use High Bit rate **Bug in Chip
    U1MODEbits.PDSEL =  0;;    //8 bit no parity
    U1MODEbits.STSEL =  0;    //One stop bit
    
    IPC2bits.U1RXIP =5;//2;//Set UART1 RX Int Priority 0-7, 0=disabled 7=Highest
	IPC2bits.U1TXIP =4;//TX Priority 2
	//IEC0bits.U1RXIE = 1;//Enable UART1 Receiver Interrupt
   	//IEC0bits.U1TXIE = 1;//Enable UART1 Tx Interrupt
   	IFS0bits.U1RXIF =0; //Set UART1 RX Int Flag=0
    
    
    U1STAbits.UTXISEL0 = 0; 
    U1STAbits.UTXISEL1 = 0; 
	U1STAbits.UTXBRK  = 0; 
	U1STAbits.URXISEL = 0; 
    //U1STAbits.UTXEN   = 1;   // Enable transmit
     
    U1BRG = 16; // 115200 baud  16 MHz Crystal
     
	U1MODEbits.UARTEN=  1;    //UART1 Enabled
     
    IEC0bits.U1RXIE = 1;//Enable UART1 Receiver Interrupt
	IEC0bits.U1TXIE = 1;//Enable UART1 Tx Interrut
	
	U1STAbits.UTXEN   = 1;   // Enable transmit
    
} 
 
 void putHex(int x) { 
	while(TXflag==0) continue;  //Checks if Buffer is Full and w8ts for it to have one location empty 
		U1TXREG=x;
		TXflag=0;
    return;     //Loops till Transmition is over 
}   
int getHex(void){
	while(RXflag==0) continue;//Loops till RX Flag=1 meaning one character has been recived 
		RXflag=0;    //resets RX flag 
    return(U1RXREG);  
}

    
void __attribute__((__interrupt__, __auto_psv__)) _U1RXInterrupt(void){    
	RXflag=1;
	_U1RXIF=0; //clear RX Interrupt Flag
}
void __attribute__((__interrupt__, __auto_psv__)) _U1TXInterrupt(void){
	TXflag=1;
	_U1TXIF=0; //clear RX Interrupt Flag

}
void clearRXBuffer(void){
	char dummy;
	dummy=U1RXREG;
	dummy=U1RXREG;
	dummy=U1RXREG;		
	}		
