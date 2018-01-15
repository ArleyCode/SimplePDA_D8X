//=============================================================================
// CASIO Mobile Device  OBReadLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// min_max.h                                                        +
//-----------------------------------------------------------------------------
#ifndef _MIN_MAX_
#define _MIN_MAX_

//
// parameter max/min
//

// scan digit number
#define RESV					0xFF	// reserve
#define OBR_DNOT				0x00	// reserve area data
#define CD39MIN0				1		// Code39	min digit (enable only Code39)
#define CD39MIN					2		// Code39	min digit
#define CD39MAX					49		// Code39	max digit
#define NW_7MIN0				1		// NW-7		min digit (enable only NW-7)
#define NW_7MIN					2		// NW-7		min digit
#define NW_7MAX					65		// NW-7		max digit (do not include Start/Stop)
#define WPCAMIN					10		// WPC addon min digit
#define WPCAMAX					18		// WPC addon max digit
#define WPC_MIN					8		// WPC		min digit
#define WPC_MAX					13		// WPC		max digit
#define UPCAMIN					9		// UPC-E addon		min digit
#define UPCAMAX					12		// UPC-E addon		max digit
#define UPC_MIN					7		// UPC-E			min digit
#define UPC_MAX					7		// UPC-E			max digit
#define IDF_MIN					2		// Industrial 2of5	min digit
#define IDF_MAX					54		// Industrial 2of5	max digit
#define ITF_MIN0				2		// Interleaved 2of5	min digit (enable only ITF)
#define ITF_MIN					4		// Interleaved 2of5	min digit
#define ITF_MAX					96		// Interleaved 2of5	max digit
#define CD93MIN					1		// Code93	min digit
#define CD93MAX					73		// Code93	max digit
#define C128MIN					1		// Code128	min digit
#define C128MAX					98		// Code128	max digit
#define MSI_MIN					1		// MSI		min digit
#define MSI_MAX					84		// MSI		max digit
#define IATA_MIN0				1		// IATA		min digit0
#define IATA_MIN1				4		// IATA		min digit default 4 (prevent mis-scan)
#define IATA_MAX				55		// IATA		max digit
#define RSS14_MIN				14		// RSS-14		min digit
#define RSS14_MAX				14		// RSS-14		max digit
#define RSSLTD_MIN				14		// RSS Limited	min digit
#define RSSLTD_MAX				14		// RSS Limited	max digit
#define RSSEXP_MIN				1		// RSS Expanded	min digit
#define RSSEXP_MAX				74		// RSS Expanded	max digit

// Code93/Code128 default min digit
// 
#define CD93DEF					3		// CODE-93  min digit for default
#define C128DEF					2		// CODE-128 min digit for default

// count / time
#define MAX_COUNT_CHECK			9		// check count max value
#define MIN_COUNT_CHECK			1		// check count min value
#define MAX_COUNT_READ			9		// scan count max value
#define MIN_COUNT_READ			1		// scan count min value
#define MAX_TIME_SCAN			9		// scan timeout max value (sec)
#define MIN_TIME_SCAN			1		// scan timeout min value (sec)
#define MAX_TIME_SCAN_API		300		// scan timeout max value by API (sec)
#define MAX_TIME_NOISE_FILTER	8		// time max value until noise filter start (sec)
#define MIN_TIME_NOISE_FILTER	1		// time min value until noise filter start (sec)

#endif//!_MIN_MAX_
