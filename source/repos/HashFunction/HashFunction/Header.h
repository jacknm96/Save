#pragma once
#include <iostream>
#include <functional>

typedef std::function< unsigned int(const char*, unsigned int)> HashExercise;

unsigned int badHash(const char* data, unsigned int length);

unsigned int GoodHash(const char* data, unsigned int length);

static HashExercise defaultHash = badHash;
