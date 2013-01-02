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
		PerlinMapWrapper(int size, array<double> ^tiles, array<double> ^decorators) : size(size)
		{
			vector<double> tiles_c;
			vector<double> decorators_c;

			for (int i = 0; i < tiles->Length; i++)
			{
				double f = tiles[i];
				tiles_c.push_back(f);
			}

			for (int i = 0; i < decorators->Length; i++)
			{
				double f = decorators[i];
				decorators_c.push_back(f);
			}

			Distribution *distr = new Distribution(tiles_c, decorators_c);

			map = new PerlinMap(size, size, distr);
		}

		~PerlinMapWrapper()
		{
			delete map;
		}

		array<Pair<int, int>^> ^placePlayers(int nbPlayers, array<int> ^inaccessibleTerrains)
		{
			vector<int> inaccessibleTerrains_c;
			for (int i = 0; i < inaccessibleTerrains->Length; i++)
			{
				int terrain = inaccessibleTerrains[i];
				inaccessibleTerrains_c.push_back(terrain);
			}

			vector<pair<int, int>> positions_c = map->placePlayers(nbPlayers, inaccessibleTerrains_c);
			array<Pair<int, int>^> ^positions;

			for (int i = 0; i < positions_c.size(); i++)
			{
				Pair<int, int> ^position = gcnew Pair<int, int>(positions_c[i].first, positions_c[i].second);

				positions[i] = position;
			}

			return positions;
		}

		int getTile(int x, int y)
		{
			return map->getTile(x, y);
		}

		int getDecorator(int x, int y)
		{
			return map->getDecorator(x, y);
		}

		int getSize()
		{
			return size;
		}
	private:
		PerlinMap *map;
		int size;
	};
}
