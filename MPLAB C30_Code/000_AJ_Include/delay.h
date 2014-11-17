
/*
If you ever worried about finding the right parameter for __delay32(), feel free to use the attached file. 
Before you can use it, rename it do delay.h. 

The file contains macros for delay_us(x) and delay_ms(x) 
*/

// FOSC changed by me on 27 July 2014

/*
	delay_us(x) and delay_ms(x) for C30
*/

#ifndef __DELAY_H

	#define FOSC		96000000LL //32000000LL		// clock-frequecy in Hz with suffix LL (64-bit-long), eg. 32000000LL for 32MHz
	#define FCY      	(FOSC/2)		// MCU is running at FCY MIPS

	#define delay_us(x)	__delay32(((x*FCY)/1000000L))	// delays x us
	#define delay_ms(x)	__delay32(((x*FCY)/1000L))		// delays x ms

	#define __DELAY_H	1
	#include <libpic30.h>

#endif
