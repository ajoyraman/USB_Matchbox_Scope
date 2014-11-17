//Aj OCxPWM.c
//   22 July 2013

//------------------------------------------------
// Sets up the OC1 & OC2 PWM Channels
// OC1RS=(0-1024) sets the duty cycle for OC1PWM
// OC2RS=(0-1024) sets the duty cycle for OC2PWM
// Default PWM is 50%
// Frequency is 23.44kHz with 12MHz crystal PLL_ON
// Step4 is a repetition but OK
// -----------------------------------------------

#include <p30f2020.h>


void setupOC1PWM(void);
void setupOC2PWM(void);

	

void setupOC1PWM(void){
		//1. Set the PWM period by writing to the appropriate period register.
		PR2=1024;
		
		//2. Set the PWM duty cycle by writing to the OCxRS register.
		OC1RS=512;
		
		//3. Configure the output compare module for PWM operation.
		OC1CONbits.OCM=6; //110 = PWM mode on OCx, Fault pin disabled
		OC1CONbits.OCTSEL=0; //0 = Timer2 is the clock source for compare x
		
		//4. Set the TMRx prescale value and enable the Timer, TON (TxCON<15>) = 1
		T2CONbits.TCS=0; //Timer2 Clock Source Internal
		T2CONbits.TCKPS=0; //Timer2 prescale 1:1
		T2CONbits.TON=1;//Enable Timer	
	
}
void setupOC2PWM(void){
		//1. Set the PWM period by writing to the appropriate period register.
		PR2=1024;
		
		//2. Set the PWM duty cycle by writing to the OCxRS register.
		OC2RS=512;
		
		//3. Configure the output compare module for PWM operation.
		OC2CONbits.OCM=6; //110 = PWM mode on OCx, Fault pin disabled
		OC2CONbits.OCTSEL=0; //0 = Timer2 is the clock source for compare x
		
		//4. Set the TMRx prescale value and enable the Timer, TON (TxCON<15>) = 1
		T2CONbits.TCS=0; //Timer2 Clock Source Internal
		T2CONbits.TCKPS=0; //Timer2 prescale 1:1
		T2CONbits.TON=1;//Enable Timer	
}		
