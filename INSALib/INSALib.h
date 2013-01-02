#if defined DLL_EXPORT
#define EXPORT_OR_IMPORT __declspec (dllexport)
#else
#define EXPORT_OR_IMPORT __declspec (dllimport)
#endif

#include <vector>
#include <iostream>

using namespace std;

double interpolate_2d(double a, double b, double c, double d, double x, double y);
inline double interpolate(double a, double b, double x);

class EXPORT_OR_IMPORT Distribution
{
public:
	/**
	 * @param tiles list of tiles frequencies.
	 * The order defines the height of the terrain : the head of the list is the lowest one, the last is the highest.
	 * Typically : water, desert, plain, mountains.
	 * @param decorators list of decorators frequencies.
	 */
	Distribution(vector<double> &terrains, vector<double> &decorators);

	/**
	 * @param r value between 0 and 1 defining the height of the terrain.
	 * @return the tile index and the decorator index corresponding to this terrain.
	 * The tile value is picked according to the height, the decorator value is purely random.
	 */
	pair<int, int> createCase(double r);

private:
	vector<double> terrains;
	vector<double> decorators;
};

// TODO: city placement suggestions

class EXPORT_OR_IMPORT PerlinMap
{
public:
	PerlinMap(int height, int width, Distribution *distr);
	int getTerrain(int x, int y);
	int getDecorator(int x, int y);

	/**
	 * @param nbPlayers number of players on the map (2 or 4).
	 * @param inaccessibleTerrains list of terrains on which units cannot be moved and cities built (typically water).
	 * @return positions of the players on the map.
	 */
	vector<pair<int, int>> getStartingPositions(vector<int> inaccessibleTerrains);
private:
	Distribution *distr;
	int width;
	int height;
	vector<vector<pair<int, int>>> cases;

	static vector<vector<double>> generateWhiteNoise(int width, int height);
	static vector<vector<double>> generatePerlinNoise(int width, int height, int octaveCount, double persistance);
};
