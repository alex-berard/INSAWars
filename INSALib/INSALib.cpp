
#include "stdafx.h"
#include "INSALib.h"

using namespace std;

int PerlinMap::getTile(int x, int y)
{
	return cases[x][y].first;
}

int PerlinMap::getDecorator(int x, int y)
{
	return cases[x][y].second;
}

PerlinMap::PerlinMap(int width, int height, Distribution *distr) : cases(), distr(distr)
{
	vector<vector<float>> perlinNoise = generatePerlinNoise(width, height, 6, 0.7f);

	for (int i = 0; i < width; i++)
	{
		vector<pair<int, int>> col;
		
		for (int j = 0; j < height; j++)
		{
			col.push_back(distr->createCase(perlinNoise[i][j]));
		}

		cases.push_back(col);
	}
}

vector<vector<float>> PerlinMap::generateWhiteNoise(int width, int height)
{
	srand(0);
	vector<vector<float>> whiteNoise;

	for (int i = 0; i < width; i++)
	{
		vector<float> v;

		for (int j = 0; j < height; j++)
		{
			v.push_back(rand() / (float) RAND_MAX);
		}

		whiteNoise.push_back(v);
	}

	return whiteNoise;
}

vector<vector<float>> PerlinMap::generatePerlinNoise(int width, int height, int octaveCount, float persistance)
{
	vector<vector<float>> baseNoise = generateWhiteNoise(width, height);
	vector<vector<float>> perlinNoise;

	float amplitude = 1;
	float total = 0;

	// Fill the perlin noise array with 0
	for (int i = 0; i < width; i++)
	{
		vector<float> v;

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

		int period = octave * octave;
		int width = baseNoise.size();
		int height = baseNoise[0].size();

		for (int i = 0; i < width; i++)
		{
			int i0 = i - i % period;
			int i1 = (i0 + period) % width;
			float x = (i - i0) / (float) period;

			for (int j = 0; j < height; j++)
			{
				int j0 = j - j % period;
				int j1 = (j0 + period) % height;
				float y = (j - j0) / (float) period;

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

float interpolate_2d(float a, float b, float c, float d, float x, float y)
{
	float i1 = interpolate(a, b, x);
	float i2 = interpolate(c, d, x);
	return interpolate(i1, i2, y);
}

inline float interpolate(float a, float b, float x)
{
	float f = (1 - cos(x * atan(1.0f) * 4)) / 2;
	return a * (1 - f) + b * f;
}

Distribution::Distribution(vector<pair<int, float>> &tiles, vector<pair<int, float>> &decorators) : tiles(tiles), decorators(decorators)
{
	// Arrange the weights into a cumulative distribution.
	for (unsigned int i = 1; i < decorators.size(); i++) {
		decorators[i].second += decorators[i - 1].second;
	}

	// TODO: real distribution according to the Perlin noise distribution.
	for (unsigned int i = 1; i < tiles.size(); i++) {
		tiles[i].second += tiles[i - 1].second;
	}
}

pair<int, int> Distribution::createCase(float r)
{
	unsigned int i, decorator, tile;

	float randNumber = rand() / (float) RAND_MAX;

	for (i = 0; i < decorators.size() - 1; i++)
	{
		if (randNumber < decorators[i].second)
		{
			break;
		}
	}

	decorator = decorators[i].first;

	for (i = 0; i < tiles.size() - 1; i++)
	{
		if (r < tiles[i].second)
		{
			break;
		}
	}

	tile = tiles[i].first;

	return pair<int, int>(tile, decorator);
}