#if defined DLL_EXPORT
#define PERLIN_MAP_EXP __declspec (dllexport)
#else
#define PERLIN_MAP_EXP __declspec (dllimport)
#endif

#include <vector>
#include <iostream>

using namespace std;

float interpolate_2d(float a, float b, float c, float d, float x, float y);
inline float interpolate(float a, float b, float x);

class PERLIN_MAP_EXP Distribution
{
public:
	/**
	 * @param tiles list of couples tile, frequency.
	 * The order defines the height of the terrain : the head of the list is the lowest one, the last is the highest.
	 * Typically : water, desert, plain, mountains. If the sum of frequencies exceeds 1, the list is truncated.
	 * @param decorators list of couples decorator, probability of having this decorator on a case.
	 */
	Distribution(vector<pair<int, float>> &tiles, vector<pair<int, float>> &decorators);

	/**
	 * @param r value between 0 and 1 defining the height of the terrain.
	 * @return couple tile, decorator corresponding to this terrain.
	 * The tile value is picked according to the height, the decorator value is purely random.
	 */
	pair<int, int> createCase(float r);

private:
	vector<pair<int, float>> tiles;
	vector<pair<int, float>> decorators;
};

class PERLIN_MAP_EXP PerlinMap
{
public:
	PerlinMap(int height, int width, Distribution *distr);
	int getTile(int x, int y);
	int getDecorator(int x, int y);
private:
	Distribution *distr;
	int width;
	int height;
	vector<vector<pair<int, int>>> cases;

	static vector<vector<float>> generateWhiteNoise(int width, int height);
	static vector<vector<float>> generatePerlinNoise(int width, int height, int octaveCount, float persistance);
};