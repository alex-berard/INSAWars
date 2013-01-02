// INSAWrapper.h

#pragma comment (lib, "INSALib.lib")
#pragma once

#include <iostream>
#include <vector>
#include "INSALib.h"

using namespace System;
using namespace std;

namespace INSAWrapper {
	public ref class PerlinMapWrapper
	{
	public:
		PerlinMapWrapper(int size, array<double>^ tiles, array<double>^ decorators)
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

		array<int, 2>^ GetStartingPositions(array<int>^ inaccessibleTerrains)
		{
			vector<int> inaccessibleTerrains_c;
			for (int i = 0; i < inaccessibleTerrains->Length; i++)
			{
				int terrain = inaccessibleTerrains[i];
				inaccessibleTerrains_c.push_back(terrain);
			}

			vector<pair<int, int>> positions_c = map->getStartingPositions(inaccessibleTerrains_c);
			array<int, 2>^ positions = gcnew array<int, 2>(positions_c.size(), 2);;

			for (int i = 0; i < positions_c.size(); i++)
			{
				positions[i, 0] = positions_c[i].first;
				positions[i, 1] = positions_c[i].second;
			}

			return positions;
		}

		int GetTerrain(int x, int y)
		{
			return map->getTerrain(x, y);
		}

		int GetDecorator(int x, int y)
		{
			return map->getDecorator(x, y);
		}
	private:
		PerlinMap *map;
	};
}
