// INSAWrapper.h

#pragma comment (lib, "INSALib.lib")
#pragma once

#include <iostream>
#include <vector>
#include "INSALib.h"

using namespace System;
using namespace std;

namespace INSAWrapper {
	generic<typename T1, typename T2> public ref class Pair
	{
	public:
		Pair(T1 first, T2 second) : first(first), second(second)
		{}

		T1 first;
		T2 second;
	};

	public ref class PerlinMapWrapper
	{
	public:
		PerlinMapWrapper(int height, int width, array<Pair<int, float>^> ^tiles, array<Pair<int, float>^> ^decorators)
		{
			vector<pair<int, float>> tiles_c;
			vector<pair<int, float>> decorators_c;

			for (int i = 0; i < tiles->Length; i++)
			{
				int n = tiles[i]->first;
				float f = tiles[i]->second;
				tiles_c.push_back(pair<int, float>(n, f));
			}

			for (int i = 0; i < decorators->Length; i++)
			{
				int n = decorators[i]->first;
				float f = decorators[i]->second;
				decorators_c.push_back(pair<int, float>(n, f));
			}

			Distribution *distr = new Distribution(tiles_c, decorators_c);

			map = new PerlinMap(height, width, distr);
		}

		~PerlinMapWrapper()
		{
			delete map;
		}

		int getTile(int x, int y)
		{
			return map->getTile(x, y);
		}

		int getDecorator(int x, int y)
		{
			return map->getDecorator(x, y);
		}
	private:
		PerlinMap *map;
	};
}
