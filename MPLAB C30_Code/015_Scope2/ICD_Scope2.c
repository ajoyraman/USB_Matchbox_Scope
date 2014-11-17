//-----------------------------------------------------------------------------
//-------------Interface Control Document -------------------------------------
//--------------------- AjScope2 ----------------------------------------------
//------------------ 20 march 2014 --------------------------------------------
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//------------------Uses the Microchip PIC 30f2020 ----------------------------
//---------------Serial Communication at 11500 bps  8bit No parity 1 Stop Bit--
//-----------------------------------------------------------------------------
//
//-----------------------------Shows commands with typical responses ----------
//
if (input_string[0]==73){ 	}	//"I" Identify:  Returns 'Aj Scope Ready' 
20-Mar-14 15:31:42.298 [TX] - I
20-Mar-14 15:31:42.323 [RX] - Aj Scope Ready <CR><LF>
else if (input_string[0]==65){ } //"A" Abort: Software Reset Returns 'AjScope'
20-Mar-14 15:35:24.580 [TX] - A
20-Mar-14 15:35:24.603 [RX] - AjScope<CR><LF>
else if (input_string[0]==66){ } //"B" Read Busy: Returns 'B'
20-Mar-14 15:36:46.250 [TX] - B
20-Mar-14 15:36:46.275 [RX] - B
else if  (input_string[0]==82){ } //"R" Reads VRef AN2/AN3 10 bit msb/lsb 
//In Ascii
20-Mar-14 15:39:08.703 [TX] - R
20-Mar-14 15:39:08.728 [RX] - R<STX>³<STX>¶
//In Hex
20-Mar-14 15:39:08.703 [TX] - 52 
20-Mar-14 15:39:08.728 [RX] - 52 02 B3 02 B6 
else if (input_string[0]==83){ } //"S" Sample Rate
	if(rate == 1){}		  // 2 us/sample 		500kbps 	20us/div
	else if(rate == 2){}  // 5 us/sample 		200kbps 	50us/div
	else if(rate == 3){ } // 10 us/sample 		100kbps  	0.1ms/div
	else if(rate == 4){ } // 20 us/sample 		50kbps  	0.2ms/div
	else if(rate == 5){}  // 50 us/sample 		20kbps  	0.5ms/div
	else if(rate == 6){ } // 100 us/sample 		10kbps 		1ms/div
	else if(rate == 7){ } // 200 us/sample 		5kbps 		2ms/div
	else if(rate == 8){ } // 500 us/sample 		2kbps 		5ms/div
	else if(rate == 9){ } // 1000 us/sample 	1kbps 		10ms/div
	else if(rate == 10){ }// 2000 us/sample 	500bps 		20ms/div
	else if(rate == 11){} // 5000 us/sample 	200ps 		50ms/div
	else if(rate == 12){} // 10000 us/sample 	100bps 		0.1s/div
	else if(rate == 13){}  // 20000 us/sample 	50bps 		0.2s/div
	else if(rate == 14){}  // 50000 us/sample  	20bps 		0.5s/div
	else if(rate == 15){}  // 100000 us/sample 	10bps 		1.0s/div
	//----------------------------------Sliding Mode	
	else if(rate == 16){}  // 1us shift/sample 	1msps 		10us/div
	else if(rate == 17){ } // 0.5us shift/sample 2msps 		5us/div
	else if(rate == 18){ } // 0.2us shift/sample 5msps 		2us/div
	else if(rate == 19){ } // 0.1us shift/sample 10msps 	1us/div
	else if(rate == 20){ } // 0.05us shift/sample 20msps 	0.5us/div
//In Hex	
20-Mar-14 15:51:36.988 [TX] - 53 01 //Sampling Rate 1 set 10 kHz
20-Mar-14 15:51:37.005 [RX] - 53 	
20-Mar-14 15:52:40.360 [TX] - 53 02 //Sampling Rate 2  set  5kHz 
20-Mar-14 15:52:40.385 [RX] - 53 
else if  (input_string[0]==78) //"N" Noise Filter not implemented: Returns N
20-Mar-14 15:54:49.513 [TX] - N
20-Mar-14 15:54:49.540 [RX] - N	
else if (input_string[0]==76){ } //"L" Trig Level: Sets Trig level 10bit msb/lsb
//In Hex			
20-Mar-14 15:56:51.844 [TX] - 4C 02 00 //Trig level common for CH1/CH2 50%
20-Mar-14 15:56:51.862 [RX] - 4C 
else if  (input_string[0]==84){ } 	//"T" Trig Source	// 0/1/2 auto/Ch1/Ch2
//In Hex
20-Mar-14 16:00:36.688 [TX] - 54 00 //Auto
20-Mar-14 16:00:36.713 [RX] - 54 
20-Mar-14 16:00:38.751 [TX] - 54 01 //CH1
20-Mar-14 16:00:38.775 [RX] - 54 
20-Mar-14 16:00:40.641 [TX] - 54 02 //CH2
20-Mar-14 16:00:40.666 [RX] - 54
else if  (input_string[0]==80){ } 	//"P" Trig Polarity : 0/1 =Normal /Inverted
//In Hex
20-Mar-14 16:03:54.609 [TX] - 50 00 //Normal
20-Mar-14 16:03:54.636 [RX] - 50 
20-Mar-14 16:05:15.957 [TX] - 50 01 //Inverted
20-Mar-14 16:05:15.982 [RX] - 50
else if  (input_string[0]==71){ }  //"G" Gains	: Gain PGA1/PGA2	
//Gain 1/2/5 Setting 0/1/3 
//In Hex
20-Mar-14 16:08:12.891 [TX] - 47 01 01 //PGA1 Gain1/PGA2 Gain1
20-Mar-14 16:08:12.911 [RX] - 47 
20-Mar-14 16:08:21.079 [TX] - 47 01 02 //PGA1 Gain1/PGA2 Gain2
20-Mar-14 16:08:21.096 [RX] - 47 
else if (input_string[0]==79){ }  //"O" Offset Ch1 10Bit msb/lsb
//In Hex
20-Mar-14 16:12:40.657 [TX] - 4F 02 66 //CH1 offset 02 66
20-Mar-14 16:12:40.681 [RX] - 4F 	
20-Mar-14 16:13:34.860 [TX] - 6F 02 66 //CH2 offset 02 66
20-Mar-14 16:13:34.884 [RX] - 6F 
else if  (input_string[0]==70){ } //"F" Capture Mode Selection				
//In Hex
20-Mar-14 16:16:43.518 [TX] - 46 00 //0= 200 CH1 data + 200 Ch2 data 8bit
20-Mar-14 16:16:43.541 [RX] - 46 
20-Mar-14 16:16:52.547 [TX] - 46 01 //1= 400  CH1 data 8bit
20-Mar-14 16:16:52.572 [RX] - 46 
20-Mar-14 16:16:54.156 [TX] - 46 02 //2= 400  CH2 data 8bit
20-Mar-14 16:16:54.181 [RX] - 46 
else if  (input_string[0]==100)  	//"d" Delay Post Trigger: Not Implemented
20-Mar-14 16:18:54.435 [TX] - d
20-Mar-14 16:18:54.465 [RX] - d	
else if  (input_string[0]==116){ } 	//"t" Toggle LED
20-Mar-14 16:19:38.187 [TX] - t
20-Mar-14 16:19:38.214 [RX] - t
else if  (input_string[0]==67){ } 	//"C" Capture Data: 
//Acquires Data and reports 'Done'
20-Mar-14 16:20:39.048 [TX] - C
20-Mar-14 16:20:39.072 [RX] - Done
else if  (input_string[0]==68){ } 	//"D" Send Data
//In Hex
20-Mar-14 16:29:06.880 [TX] - 44 01 //CH1 200	
20-Mar-14 16:29:06.920 [RX] - 80 84 87 8C 90 94 98 9B 9E A0 A1 A3 A3 A3 A3 A1 A0 9E 9B 98 94 90 8C 87 83 7F 7B 76 72 6E 6A 67 63 61 5F 5D 5C 5C 5C 5C 5D 5F 61 63 67 6A 6E 72 77 7B 7F 83 87 8C 90 94 98 9B 9E 9F A1 A2 A3 A3 A3 A1 9F 9E 9B 98 94 90 8C 87 83 7F 7B 76 71 6E 6A 67 63 60 5F 5C 5C 5B 5B 5C 5C 5E 60 63 66 69 6E 71 76 7A 7F 83 87 8C 90 93 98 9B 9D 9F A1 A2 A3 A3 A2 A1 9F 9E 9B 98 94 90 8C 87 83 7F 7B 76 72 6E 6A 67 63 61 5F 5D 5C 5C 5C 5C 5D 5F 61 63 67 6A 6E 72 77 7B 7F 84 87 8C 90 94 98 9B 9E A0 A1 A3 A3 A3 A3 A1 A0 9E 9B 98 94 90 8C 87 84 7F 7B 77 72 6E 6A 67 63 61 5F 5D 5C 5B 5B 5C 5C 5F 61 63 67 69 6E 71 76 7B 
20-Mar-14 16:29:10.270 [TX] - 44 02 //CH2 200
20-Mar-14 16:29:10.288 [RX] - 80 89 93 9B A4 AC B3 B9 BF C3 C7 C9 CB CB C9 C7 C4 C0 BB B4 AD A5 9D 94 8B 82 78 70 67 5E 57 50 48 43 3F 3C 39 38 38 38 3B 3E 42 47 4E 54 5C 65 6E 78 80 89 93 9B A3 AC B3 B9 BF C3 C7 C8 CA CB C9 C7 C4 BF BB B3 AC A5 9C 93 8B 81 78 6F 66 5E 56 4F 47 43 3E 3B 38 38 37 38 39 3C 41 47 4C 53 5C 63 6C 76 7F 87 91 9B A3 AB B1 B8 BE C2 C6 C7 C9 CA C9 C7 C3 BF BA B3 AC A5 9C 93 8B 81 78 70 66 5E 57 50 48 43 3F 3B 38 38 38 38 3B 3E 42 47 4E 54 5C 64 6E 77 80 89 93 9B A3 AC B3 B9 BF C3 C7 C9 CB CB C9 C7 C4 C0 BB B4 AD A5 9D 94 8B 82 78 70 67 5E 57 50 48 43 3F 3B 38 38 38 38 
20-Mar-14 16:31:37.018 [TX] - 44 03 //First 200 of 400 samples CH1/CH2
20-Mar-14 16:31:37.059 [RX] - 81 89 93 9B A3 AB B1 B8 BE C2 C5 C7 C9 C9 C7 C6 C3 BF B9 B3 AC A5 9C 93 8B 82 79 70 67 5F 58 50 4B 45 40 3D 3B 39 38 3A 3C 3F 43 48 4F 56 5D 65 6E 77 7F 87 90 99 A1 A9 B0 B7 BC C0 C4 C7 C7 C7 C7 C4 C1 BD B8 B1 AB A3 9C 93 8A 81 78 70 67 5F 58 50 4A 44 3F 3C 39 38 38 39 3C 3F 43 47 4F 56 5D 65 6E 77 7F 87 90 99 A2 A9 B0 B8 BC C1 C4 C7 C7 C7 C7 C5 C2 BE B8 B3 AC A4 9C 93 8B 82 79 70 67 60 58 51 4B 45 40 3D 3B 39 39 3A 3C 3F 43 49 50 56 5D 65 6E 77 7F 87 91 99 A1 A9 B0 B8 BC C1 C4 C7 C7 C7 C7 C5 C1 BE B8 B2 AC A3 9C 93 8A 81 78 70 67 5F 58 50 4A 44 3F 3C 39 38 38 39 3B 3F 43 47 4F 55 5C 64 6C 76 
20-Mar-14 16:31:38.892 [TX] - 44 04 //Second 200 of 400 samples CH1/CH2
20-Mar-14 16:31:38.934 [RX] - 7F 87 90 99 A1 A9 B0 B8 BC C1 C4 C7 C7 C7 C7 C5 C2 BE B8 B3 AC A4 9C 93 8B 82 79 70 67 60 58 51 4B 45 41 3D 3B 39 39 3B 3C 3F 44 49 50 56 5D 65 6E 77 7F 87 91 99 A1 A9 B0 B8 BC C1 C4 C7 C7 C8 C7 C5 C2 BE B8 B3 AC A4 9C 93 8B 82 78 70 67 5F 58 50 4B 44 40 3C 3A 38 38 39 3C 3F 43 47 4E 55 5C 64 6C 76 7F 87 90 98 A1 A7 B0 B6 BC C0 C4 C7 C7 C7 C7 C5 C2 BE B8 B3 AC A4 9C 93 8B 82 79 70 67 5F 58 51 4B 45 40 3D 3B 39 39 3A 3C 3F 43 48 4F 56 5D 65 6E 77 7F 87 91 99 A2 A9 B0 B8 BC C1 C4 C7 C7 C7 C7 C6 C2 BE B8 B3 AC A4 9C 93 8B 82 79 70 67 5F 58 51 4B 45 40 3D 3B 38 38 39 3C 3F 43 47 4E 54 5C 64 6C 76 			
else { 	} //Any Fake Sequence:  SW Reset
20-Mar-14 16:32:25.501 [TX] - X
20-Mar-14 16:32:25.527 [RX] - AjScope<CR><LF>

//-------------------END of ICD------------------------------------------------

