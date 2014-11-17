/*
 * Copyright 2006 Microchip Technology, all rights reserved
 * 
 * This file defines useful exports from libpic30.a
 * Not all functions defined in libpic30.a can be usefully called by
 * the user, most are helper functions for the standard C library, libc.a
 *
 */

#ifndef __LIBPIC30_H
#define __LIBPIC30_H

/*
 * The following two functions allow the programmer to attach a file to
 * standard input, and later close the file.  The functions are only useful
 * if the executable will be simulated.
 *
 * With these functions input from the file will be read on-demmand, otherwise
 * the programmer should use the message based stimulus (information on this
 * can be found within MPLAB IDE's help pages).
 */

extern int __attach_input_file(const char *f);
void __close_input_file(void);

/*
 * __C30_UART selects the default UART port that read() and write() will use.  
 * read() is called by fscanf and family, while write() is called by printf 
 * and family.  The default setting (as shipped) is 1, which is suitable for 
 * the dsPICdem 1.1(tm) board.   Modifying this to another value will select
 * UART 2, suitable for the explorer 16(tm) board.
 */

extern int __C30_UART;

/*
 * __delay32() provides a 32-bit delay routine which will delay for the number
 * of cycles denoted by its argument.  The minimum delay is 11 cycles
 * including call and return statements.  With this function only the time
 * required for argument transmission is not accounted for.  Requests for
 * less than 11 cycles of delay will cause an 11 cycle delay.
 */

extern void __delay32(unsigned long cycles);

/*
 * __memcpy_helper() - not a user-callable function
 *
 * Copies data from program memory to data memory. It does
 * not require the Program Space Visibility (PSV) window. However, it
 * does change the value of TBLPAG during execution.
 *
 * The source address in program memory is specified by a tbl_offset,
 * tbl_page pair. Flags indicate whether the upper byte should be
 * copied, and whether to terminate early after a NULL byte.
 * The return value is an updated source address pair.
 *
 * The number of bytes copied may be even or odd. The source address
 * is always even and is always aligned to the start of the next
 * program memory word.
 */
typedef struct _prog_address_tag {
  union {
    struct {
      int low;
      int high;
    } w;
    long next;
  };
} _prog_addressT;

extern _prog_addressT _memcpy_helper(unsigned int tbl_offset, unsigned int tbl_page,
                                     char *dst, unsigned int len, int flags);

/*
 * Initialize a variable of type _prog_addressT.
 *
 * These variables are not equivalent to C pointers,
 * since each source address corresponds to 2 or 3 chars.
 */

#define _init_prog_address(a,b) {      \
    a.w.low  = __builtin_tbloffset(b); \
    a.w.high = __builtin_tblpage(b);   \
  };

/*
 * _memcpy_p2d16() copies 16 bits of data from each address
 * in program memory to data memory. The source address is
 * specified as type _prog_addressT; the next unused address
 * is returned.
 */

static __inline__ _prog_addressT
_memcpy_p2d16(char *dest, _prog_addressT src, unsigned int len) {
  return _memcpy_helper(src.w.low, src.w.high, dest, len, 0);
}

/*
 * _memcpy_p2d24() copies 24 bits of data from each address
 * in program memory to data memory. The source address is
 * specified as type _prog_addressT; the next unused address
 * is returned.
 */

static __inline__ _prog_addressT
_memcpy_p2d24(char *dest, _prog_addressT src, unsigned int len) {
  return _memcpy_helper(src.w.low, src.w.high, dest, len, 1);
}

/*
 * _strncpy_p2d16() copies 16 bits of data from each address
 * in program memory to data memory. The operation terminates
 * early if a NULL char is copied. The source address is
 * specified as type _prog_addressT; the next unused address
 * is returned.
 */

static __inline__ _prog_addressT
_strncpy_p2d16(char *dest, _prog_addressT src, unsigned int len) {
  return _memcpy_helper(src.w.low, src.w.high, dest, len, 2);
}

/*
 * _strncpy_p2d24() copies 24 bits of data from each address
 * in program memory to data memory. The operation terminates
 * early if a NULL char is copied. The source address is
 * specified as type _prog_addressT; the next unused address
 * is returned.
 */

static __inline__ _prog_addressT
_strncpy_p2d24(char *dest, _prog_addressT src, unsigned int len) {
  return _memcpy_helper(src.w.low, src.w.high, dest, len, 3);
}

#endif
