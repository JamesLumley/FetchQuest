using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FetchQuest
{
    //CLASS CREATED BY CHRIS
    //CLASS EDITED BY DAN AND JAMES
    //ART CREATED BY JAMES
    static class TileMap
    {
        #region Declarations
        public const int TileWidth = 96;
        public const int TileHeight = 96;
        public const int MapWidth = 34;
        public const int MapHeight = 46;

        public const int FloorTileStart = 0;
        public const int FloorTileEnd = 2;
        public const int WallTileStart = 3;
        public const int WallTileEnd = 7;

        static private Texture2D texture,forest,dungeon,cave;
        static private List<Rectangle> tiles = new List<Rectangle>();

        static private int[,] mapSquares = new int[MapWidth, MapHeight];

        static private Random rand = new Random();
        static private int miners = 0;
        static private int MAXMINERS = 6;
        static private int floorTile = 0;

        #region Wall Tile Setting

        static private int wallTile = 2;
        static private int wallTileLT = 3;
        static private int wallTileLM = 4;
        static private int wallTileLB = 5;
        static private int wallTileRT = 6;
        static private int wallTileRM = 7;
        static private int wallTileRB = 8;
        static private int wallTileT = 9;
        static private int wallTileB = 10;
        static private int wallTileCTL = 11;
        static private int wallTileCTR = 12;
        static private int wallTileCBL = 13;
        static private int wallTileCBR = 14;

        #endregion

        #endregion

        #region Initialization
        static public void Initialize(Texture2D forestTexture, Texture2D dungeonTexture, Texture2D caveTexture)
        {
            //Adds all the tiles to the tile list
            forest = forestTexture;
            dungeon = dungeonTexture;
            cave = caveTexture;
            tiles.Clear();
            tiles.Add(new Rectangle(3 * TileWidth, TileHeight * 2, TileWidth, TileHeight));//FLOOR
            tiles.Add(new Rectangle(4 * TileWidth, TileHeight * 2, TileWidth, TileHeight));//TILES
            tiles.Add(new Rectangle(TileHeight, TileHeight, TileWidth, TileHeight));
            tiles.Add(new Rectangle(0, 0, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(0, 96, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(0, 192, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(192, 0, TileWidth, TileHeight));//WALL
            tiles.Add(new Rectangle(192, 96, TileWidth, TileHeight));//TILES
            tiles.Add(new Rectangle(192, 192, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(96, 0, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(96, 192, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(96 * 3, 0, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(96 * 4, 0, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(96 * 3, 96, TileWidth, TileHeight));//
            tiles.Add(new Rectangle(96 * 4, 96, TileWidth, TileHeight));//
        }
        static public void Generate()//Generates the random map
        {
            miners = 0;
            int selectedTileset = QuestGenerator.lostObjectLocation;
            if (selectedTileset == 0) texture = forest;
            if (selectedTileset == 1) texture = dungeon;
            if (selectedTileset == 2) texture = cave;
            FillMap();
            GenerateRandomMap(new Vector2(MapWidth / 2, MapHeight - 2));
            CleanUp();
        }
        #endregion

        #region Information about Map Squares

        static public int GetSquareByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }

        static public int GetSquareByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }

        static public Vector2 GetSquareAtPixel(Vector2 pixelLocation)
        {
            return new Vector2(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }

        static public Vector2 GetSquareCenter(int squareX, int squareY)
        {
            return new Vector2(
                (squareX * TileWidth) + (TileWidth / 2),
                (squareY * TileHeight) + (TileHeight / 2));
        }

        static public Vector2 GetSquareCenter(Vector2 square)
        {
            return GetSquareCenter(
                (int)square.X,
                (int)square.Y);
        }

        static public Rectangle SquareWorldRectangle(int x, int y)
        {
            return new Rectangle(
                x * TileWidth,
                y * TileHeight,
                TileWidth,
                TileHeight);
        }

        static public Rectangle SquareWorldRectangle(Vector2 square)
        {
            return SquareWorldRectangle(
                (int)square.X,
                (int)square.Y);
        }

        static public Rectangle SquareScreenRectangle(int x, int y)
        {
            return Camera.Transform(SquareWorldRectangle(x, y));
        }

        static public Rectangle SquareSreenRectangle(Vector2 square)
        {
            return SquareScreenRectangle((int)square.X, (int)square.Y);
        }
        #endregion

        #region Information about Map Tiles

        static public int GetTileAtSquare(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                return mapSquares[tileX, tileY];
            }
            else
            {
                return -1;
            }
        }

        static public void SetTileAtSquare(int tileX, int tileY, int tile)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                mapSquares[tileX, tileY] = tile;
            }
        }

        static public int GetTileAtPixel(int pixelX, int pixelY)
        {
            return GetTileAtSquare(
                GetSquareByPixelX(pixelX),
                GetSquareByPixelY(pixelY));
        }

        static public int GetTileAtPixel(Vector2 pixelLocation)
        {
            return GetTileAtPixel(
                (int)pixelLocation.X,
                (int)pixelLocation.Y);
        }

        static public bool IsWallTile(int tileX, int tileY)
        {
            int tileIndex = GetTileAtSquare(tileX, tileY);

            if (tileIndex == -1)
            {
                return false;
            }

            return tileIndex >= WallTileStart;
        }

        static public bool IsFloorTile(int tileX, int tileY)
        {
            if (mapSquares[tileX, tileY] == floorTile)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool IsFloorTile(Vector2 square)
        {
            return IsFloorTile((int)square.X, (int)square.Y);
        }


        static public bool IsWallTile(Vector2 square)
        {
            return IsWallTile((int)square.X, (int)square.Y);
        }

        static public bool IsWallTileByPixel(Vector2 pixelLocation)
        {
            return IsWallTile(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }

        static public bool IsInCorner(int x, int y)
        {
            int solitaryCounter = 0;

            if (mapSquares[x - 1, y] == floorTile) solitaryCounter += 1;
            if (mapSquares[x + 1, y] == floorTile) solitaryCounter += 1;
            if (mapSquares[x, y - 1] == floorTile) solitaryCounter += 1;
            if (mapSquares[x, y + 1] == floorTile) solitaryCounter += 1;

            if (solitaryCounter < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            

        #endregion

        #region Map Generation
        static public void FillMap()//Fills the map with tiles
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }
        }

        static public bool IsValidPoint(Vector2 point)
        {
            if(((int)point.X > MapWidth-3) || ((int)point.X < 2) || ((int)point.Y > MapHeight-3) || ((int)point.Y < 2))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        

        static public bool IsSolitary(int x, int y)//Checks if the wall tile is surrounded completely by floor tiles
        {
            if (IsValidPoint(new Vector2(x,y)) == true)
            {
                int solitaryCounter = 0;
                
                if (mapSquares[x - 1, y] == floorTile) solitaryCounter += 1;
                if (mapSquares[x + 1, y] == floorTile) solitaryCounter += 1;
                if (mapSquares[x, y - 1] == floorTile) solitaryCounter += 1;
                if (mapSquares[x, y + 1] == floorTile) solitaryCounter += 1;
                
                if(solitaryCounter > 2)
                {
                    return true;
                }
                else return false;
                
            }
            else return false;
        }

        static public bool PlayerSpawnSolitary(int x, int y)//Checks if the player is surrounded completely by floor tiles
        {
            int solitaryCounter = 0;
            if (x > 0 && x < MapWidth - 1 && y > 0 && y < MapHeight-1)
            {
                if (mapSquares[x - 1, y - 1] == floorTile) solitaryCounter++;
                if (mapSquares[x, y - 1] == floorTile) solitaryCounter++;
                if (mapSquares[x + 1, y - 1] == floorTile) solitaryCounter++;

                if (mapSquares[x - 1, y] == floorTile) solitaryCounter++;
                if (mapSquares[x, y] == floorTile) solitaryCounter++;
                if (mapSquares[x + 1, y] == floorTile) solitaryCounter++;

                if (mapSquares[x - 1, y + 1] == floorTile) solitaryCounter++;
                if (mapSquares[x, y + 1] == floorTile) solitaryCounter++;
                if (mapSquares[x + 1, y + 1] == floorTile) solitaryCounter++;
            }
            if (solitaryCounter > 4) return true;
            else return false;
        }
        
        static public void GenerateRandomMap(Vector2 dig)//Generates the random map using a miner algorithm
        {
            bool complete = false;
            while(complete == false)
            {
                if (miners < MAXMINERS)
                {
                    if (rand.Next(1, 300) == 1)
                    {
                        miners++;
                        GenerateRandomMap(dig);
                    }
                }
                Vector2 direction = new Vector2(0, 0);
                int directions = rand.Next(0,4)+1;
                if (directions == 1)
                {
                    direction = new Vector2(1, 0);
                }
                if (directions == 2)
                {
                    direction = new Vector2(0, 1);
                }
                if (directions == 3)
                {
                    direction = new Vector2(-1, 0);
                }
                if (directions == 4)
                {
                    direction = new Vector2(0, -1);
                }
                Vector2 newDig = dig + direction;
                if(IsValidPoint(newDig))
                {
                    mapSquares[(int)newDig.X, (int)newDig.Y] = floorTile;
                    dig = newDig;
                }
                if (((mapSquares[(int)dig.X - 1, (int)dig.Y] == floorTile) && (mapSquares[(int)dig.X + 1, (int)dig.Y] == floorTile) &&
                    (mapSquares[(int)dig.X, (int)dig.Y - 1] == floorTile) && (mapSquares[(int)dig.X, (int)dig.Y + 1] == floorTile))&& (miners == MAXMINERS))
                {
                    complete = true;
                }
            }         
        }
        //EDITED BY DAN{
        static public void CleanUp()//Cleans up the map to place the correct tile types in the correct places
        {
            int counter;
            for (counter = 0; counter < 10; counter++)
            {
                for (int i = 0; i < MapWidth; i++)
                {
                    for (int j = 0; j < MapHeight; j++)
                    {
                        if (mapSquares[i, j] == wallTile)
                        {
                            //if (i > 0 && i < MapWidth - 9 && j > 0 && j < MapHeight - 9) CleanFillMap(i, j);
                            if (counter < 8)
                            {
                                if (IsSolitary(i, j) == true)
                                {
                                    mapSquares[i, j] = floorTile;
                                }
                                if (i > 0 && i < MapWidth - 1 && j > 0 && j < MapHeight - 1)
                                {
                                    CornerGlitchFix(i, j);
                                    CornerGlitchFix(i, j);
                                }
                            }
                            if (i > 0 && i < MapWidth - 1 && j > 0 && j < MapHeight - 1)
                            {
                                if (counter == 8) EdgeAssign(i, j);
                                if (counter == 9) CornerAssign(i, j);
                            }
                        }

                    }
                }
            }
        }

        /*static public void CleanFillMap(int x, int y)
        {
            int fillMapCount = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mapSquares[x + i, y + j] == floorTile) fillMapCount += 1;
                }
            }
            if (fillMapCount == 9 * 9)
            {
                for (int i = 2; i < 7; i++)
                {
                    for (int j = 2; j < 7; j++)
                    {
                        mapSquares[x + i, y + j] = wallTile;
                    }
                }
            }
        }*/

        static public void EdgeAssign(int x, int y)//Asigns map edges
        {
            bool right = false, left = false, top = false, bottom = false;

            if (mapSquares[x - 1, y] == floorTile) left = true;
            if (mapSquares[x + 1, y] == floorTile) right = true;
            if (mapSquares[x, y + 1] == floorTile) bottom = true;
            if (mapSquares[x, y - 1] == floorTile) top = true;

            if (top == true) mapSquares[x, y] = wallTileT;
            if (bottom == true) mapSquares[x, y] = wallTileB;

            if (left == true)
            {
                mapSquares[x, y] = wallTileLM;
                if (top == true) mapSquares[x, y] = wallTileLT;
                if (bottom == true) mapSquares[x, y] = wallTileLB;
            }

            if (right == true)
            {
                mapSquares[x, y] = wallTileRM;
                if (top == true) mapSquares[x, y] = wallTileRT;
                if (bottom == true) mapSquares[x, y] = wallTileRB;
            }
        }

        static public void CornerAssign(int x, int y)
        {
            int right = 0, left = 0;

            if (mapSquares[x, y - 1] == wallTileLM || mapSquares[x, y - 1] == wallTileLT) left = 1;
            if (mapSquares[x, y + 1] == wallTileLM || mapSquares[x, y + 1] == wallTileLB) left = -1;
            if (mapSquares[x, y - 1] == wallTileRM || mapSquares[x, y - 1] == wallTileRT) right = 1;
            if (mapSquares[x, y + 1] == wallTileRM || mapSquares[x, y + 1] == wallTileRB) right = -1;

            if (left == -1) mapSquares[x, y] = wallTileCTR;
            if (left == 1) mapSquares[x, y] = wallTileCBR;
            if (right == -1) mapSquares[x, y] = wallTileCTL;
            if (right == 1) mapSquares[x, y] = wallTileCBL;

        }

        static public void CornerGlitchFix(int x, int y)
        {
            if (mapSquares[x, y] == wallTile)
            {
                if (mapSquares[x - 1, y - 1] == floorTile && mapSquares[x + 1, y + 1] == floorTile) mapSquares[x, y] = floorTile;
                if (mapSquares[x, y - 1] == floorTile && mapSquares[x, y + 1] == floorTile) mapSquares[x, y] = floorTile;
                if (mapSquares[x + 1, y - 1] == floorTile && mapSquares[x - 1, y + 1] == floorTile) mapSquares[x, y] = floorTile;
                if (mapSquares[x + 1, y] == floorTile && mapSquares[x - 1, y] == floorTile) mapSquares[x, y] = floorTile;
            }
        }
       //} EDITED BY DAN
        //EDITED BY JAMES{
        static public void TutorialGenerateMap()//Generates the map for the tutorial with manual placements (not random)
        {
            int floorTile = 0;
            int wallTile = 2;
            texture = dungeon;


            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    mapSquares[x, y] = floorTile;
                    if (x == 0)
                    {
                        mapSquares[x, y] = 11;
                        continue;
                    }

                    else if (y == 0)
                    {
                        mapSquares[x, y] = 10;
                        continue;
                    }
                    else if (x == MapWidth - 1)
                    {
                        mapSquares[x, y] = 12;
                        continue;
                    }

                    else if (y == MapHeight - 1)
                    {
                        mapSquares[x, y] = 9;
                        continue;
                    }

                }
            }

            for (int y = 0; y <= 45; y++)
            {
                mapSquares[0, y] = wallTile;
            }
            for (int y = 7; y <= 8; y++)
            {
                mapSquares[2, y] = 5;
            }
            for (int x = 0; x <= 13; x++)//top left wall set
            {
                for (int y = 0; y <= 6; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int y = 0; y <= 6; y++)
            {
                mapSquares[13, y] = 11;
            }

            for (int x = 26; x <= 33; x++)//top right wall set
            {
                for (int y = 0; y <= 6; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int y = 0; y <= 6; y++)
            {
                mapSquares[26, y] = 12;
            }


            for (int x = 0; x <= 16; x++)//set of walls between top corridor (to boss room) and middle area (left side)
            {
                for (int y = 7; y <= 8; y++)
                {
                    mapSquares[x, y] = wallTile;
                }

            }


            for (int x = 20; x <= 33; x++)//set of walls between top corridor (to boss room) and middle area (right side)
            {
                for (int y = 7; y <= 8; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 0; x <= 9; x++)//wall set above the left most room
            {
                for (int y = 6; y <= 10; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }


            for (int x = 7; x <= 9; x++)//wall set above the left most room that extends to the entrance to the room
            {
                for (int y = 13; y <= 22; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 0; x <= 9; x++)//wall set below the left most room
            {
                for (int y = 20; y <= 39; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 0; x <= 9; x++)//bottom left wall set
            {
                for (int y = 26; y <= 45; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 25; x <= 33; x++)//bottom right wall set
            {
                for (int y = 38; y <= 45; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 9; x <= 16; x++)//set of walls between bottom corridor (to starting room) and middle area (left side)
            {
                for (int y = 38; y <= 40; y++)
                {
                    mapSquares[x, y] = wallTile;
                }

            }

            for (int x = 21; x <= 29; x++)//set of walls between bottom corridor (to starting room) and middle area (right side)
            {
                for (int y = 38; y <= 40; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }



            for (int x = 7; x <= 9; x++)//wall set below the left most room that extends to the entrance to the room
            {
                for (int y = 23; y <= 30; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }


            for (int x = 26; x <= 33; x++)//set of walls in the top right of the middle area
            {
                for (int y = 23; y <= 24; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }
            for (int x = 26; x <= 28; x++)//set of walls in the top right of the middle area
            {
                for (int y = 15; y <= 23; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 17; x <= 28; x++)//top middle wall
            {
                for (int y = 15; y <= 17; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 17; x <= 18; x++)//left middle wall
            {
                for (int y = 17; y <= 32; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 19; x <= 26; x++)//bottom middle wall
            {
                for (int y = 29; y <= 32; y++)
                {
                    mapSquares[x, y] = wallTile;
                }
            }

            for (int x = 27; x <= 29; x++)//bottom middle right wall
            {
                mapSquares[x, 29] = wallTile;
                mapSquares[x, 30] = wallTile;
                mapSquares[x, 31] = wallTile;
                mapSquares[x, 32] = wallTile;
            }

            //Extra walls for sizing purposes
            /*  for (int y = 0; y <= 45; y++)//right wall
              {
                  mapSquares[33, y] = wallTile;
              }*/

            for (int y = 0; y <= 45; y++)//left wall
            {
                mapSquares[1, y] = wallTile;
            }

            CleanUp();
            Level1Generator.EnemyGeneration();
            //}EDITED BY JAMES
        }

        #endregion

        #region Drawing
        static public void Draw(SpriteBatch spriteBatch)
        {
            int startX = GetSquareByPixelX((int)Camera.Position.X);
            int endX = GetSquareByPixelX((int)Camera.Position.X +
                  Camera.ViewPortWidth);

            int startY = GetSquareByPixelY((int)Camera.Position.Y);
            int endY = GetSquareByPixelY((int)Camera.Position.Y +
                      Camera.ViewPortHeight);

            for (int x = startX; x <= endX; x++)
                for (int y = startY; y <= endY; y++)
                {
                    if ((x >= 0) && (y >= 0) &&
                        (x < MapWidth) && (y < MapHeight))
                    {
                        spriteBatch.Draw(
                            texture,
                            SquareScreenRectangle(x, y),
                            tiles[GetTileAtSquare(x, y)],
                            Color.White);
                    }
                }
        }
        #endregion
    }
}
