
#include "INSALib.h"

using namespace std;

int PerlinMap::getTerrain(int x, int y)
{
	return cases[x][y].first;
}

int PerlinMap::getDecorator(int x, int y)
{
	return cases[x][y].second;
}

PerlinMap::PerlinMap(int width, int height, Distribution *distr) : cases(), distr(distr), width(width), height(height)
{
	vector<vector<double>> perlinNoise = generatePerlinNoise(width, height, 6, 0.7);

	cases = distr->createMap(perlinNoise);
}

vector<vector<double>> PerlinMap::generateWhiteNoise(int width, int height)
{
	srand(time(NULL));
	vector<vector<double>> whiteNoise;

	for (int i = 0; i < width; i++)
	{
		vector<double> v;

		for (int j = 0; j < height; j++)
		{
			v.push_back(rand() / (double) RAND_MAX);
		}

		whiteNoise.push_back(v);
	}

	return whiteNoise;
}

vector<vector<double>> PerlinMap::generatePerlinNoise(int width, int height, int octaveCount, double persistance)
{
	vector<vector<double>> baseNoise = generateWhiteNoise(width, height);
	vector<vector<double>> perlinNoise;

	double amplitude = 1;
	double total = 0;

	// Fill the perlin noise array with 0
	for (int i = 0; i < width; i++)
	{
		vector<double> v;

		for (int j = 0; j < height; j++)
		{
			v.push_back(0);
		}

		perlinNoise.push_back(v);
	}

	// Compute the octaves
	for (int octave = octaveCount - 1; octave >= 0; octave--) {
		amplitude *= persistance;
		total += amplitude;

		int period = pow(2, octave);
		int width = baseNoise.size();
		int height = baseNoise[0].size();

		for (int i = 0; i < width; i++)
		{
			int i0 = i - i % period;
			int i1 = (i0 + period) % width;
			double x = (i - i0) / (double) period;

			for (int j = 0; j < height; j++)
			{
				int j0 = j - j % period;
				int j1 = (j0 + period) % height;
				double y = (j - j0) / (double) period;

				 perlinNoise[i][j] += amplitude * interpolate_2d(
					baseNoise[i0][j0],
					baseNoise[i1][j0],
					baseNoise[i0][j1],
					baseNoise[i1][j1],
					x, y);
			}
		}
	}

	// Normalize the perlin noise array
	for (int i = 0; i < width; i++)
	{
		for (int j = 0; j < height; j++)
		{
			perlinNoise[i][j] /= total;
		}
	}

	return perlinNoise;
}

double interpolate_2d(double a, double b, double c, double d, double x, double y)
{
	double i1 = interpolate(a, b, x);
	double i2 = interpolate(c, d, x);
	return interpolate(i1, i2, y);
}

inline double interpolate(double a, double b, double x)
{
	double f = (1 - cos(x * atan(1.0) * 4)) / 2;
	return a * (1 - f) + b * f;
}

Distribution::Distribution(vector<double>& terrains, vector<double>& decorators) : terrains(terrains), decorators(decorators)
{
	// Arrange the weights into a cumulative distribution.
	for (int i = 1; i < decorators.size(); i++) {
		decorators[i] += decorators[i - 1];
	}
}

bool compPositionsHeights(pair<pair<int, int>, double> p1, pair<pair<int, int>, double> p2)
{
	return p1.second < p2.second;
}

// TODO: smooth
vector<vector<pair<int, int>>> Distribution::createMap(vector<vector<double>>& perlinMap)
{
	vector<vector<pair<int, int>>> map;
	vector<pair<pair<int, int>, double>> sortedPositions;

	for (int i = 0; i < perlinMap.size(); i++)
	{
		vector<pair<int, int>> col;

		for (int j = 0; j < perlinMap[0].size(); j++)
		{
			col.push_back(pair<int, int>(0, -1));
			pair<int, int> position(i, j);
			pair<pair<int, int>, double> positionHeight(position, perlinMap[i][j]);
			sortedPositions.push_back(positionHeight);
		}

		map.push_back(col);
	}

	sort(sortedPositions.begin(), sortedPositions.end(), compPositionsHeights);

	int j = 0;
	for (int i = 0; i < terrains.size(); i++)
	{
		int n = sortedPositions.size() * terrains[i];
		int end = (i == terrains.size() - 1) ? sortedPositions.size() : j + n;

		for (; j < end; j++)
		{
			pair<int, int> pos = sortedPositions[j].first;
			map[pos.first][pos.second].first = i;

			double randNumber = rand() / (double) RAND_MAX;

			for (int k = 0; k < decorators.size(); k++)
			{
				if (randNumber < decorators[k])
				{
					map[pos.first][pos.second].second = k;
					break;
				}
			}
		}
	}

	return map;
}

vector<pair<int, int>> PerlinMap::getStartingPositions(vector<int> inaccessibleTerrains)
{
	// TODO: do not place players on the water + randomize
	vector<pair<int, int>> positions;
	positions.push_back(pair<int, int>(0, 0));
	positions.push_back(pair<int, int>(width - 1, height - 1));
	positions.push_back(pair<int, int>(width - 1, 0));
	positions.push_back(pair<int, int>(0, height - 1));

	return positions;
}