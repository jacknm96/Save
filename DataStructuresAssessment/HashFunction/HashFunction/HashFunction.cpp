// HashFunction.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <list>

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

std::string UnHash(unsigned int hash, std::string names[]) {
	return names[hash % 20];
}

int main()
{
    std::string name = "Jack M";
	std::string names[20] = {""};
    const char* c = name.c_str();
    typedef unsigned int uint;
    unsigned int i = name.size();
    std::cout << "Name is: " << name << std::endl;
    int hash = badHash(c, i);
    std::cout << "Bad hash is: " << hash << std::endl;
    hash = GoodHash(c, i);
	for (int i = hash % 20; i < sizeof(names) / sizeof(names[0]); i++) {
		if (names[hash % 20] == "") {
			names[hash % 20] = name;
		}
	}
    std::cout << "Good hash is: " << hash << std::endl;
	std::cout << "Hashed name is: " << UnHash(hash, names) << std::endl;
	std::cin >> name;
    return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
