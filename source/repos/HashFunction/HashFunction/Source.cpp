#include <iostream>
#include "Header.h"


unsigned int badHash(const char* data, unsigned int length) {
	unsigned int hash = 0;
	for (unsigned int i = 0; i < length; i++) {
		hash += data[i];
	}
	return hash;
}

unsigned int GoodHash(const char* data, unsigned int length) {
	unsigned int hash = 0;
	for (unsigned int i = 0; i < length; i++) {
		hash *= 10;
		hash += data[i] * 13 * i;
	}
	return hash;
}