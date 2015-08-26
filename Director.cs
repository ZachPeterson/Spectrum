using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWars
{
    class Director
    {
        public GameWorld aaGameWorld;

        private ShipKit mGruntKit;
        private ShipKit mTankKit;
        private ShipKit mBombKit;
        private ShipKit mStealthKit;
        private ShipKit mLaserKit;

        private ShipKit mGruntBossKit;
        private ShipKit mTankBossKit;
        private ShipKit mBombBossKit;

        public int mTotalEnemies = 0;
        private int mTotalEnemiesSpawned = 0;

        private int mGruntsToSpawn = 0;
        private int mGruntsSpawned = 0;
        private int mTanksToSpawn = 0;
        private int mTanksSpawned = 0;
        private int mBombsToSpawn = 0;
        private int mBombsSpawned = 0;
        private int mStealthToSpawn = 0;
        private int mStealthSpawned = 0;
        private int mLasersToSpawn = 0;
        private int mLasersSpawned = 0;

        private int mGruntBossesToSpawn = 0;
        private int mGruntBossesSpawned = 0;
        private int mTankBossesToSpawn = 0;
        private int mTankBossesSpawned = 0;
        private int mBombBossesToSpawn = 0;
        private int mBombBossesSpawned = 0;

        private float mWaveLength = 0.0f;
        private float mWaveTime = 0.0f;
        private float mTimeToNextSpawn = 0.0f;

        public Director(GameWorld pGameWorld)
        {
            aaGameWorld = pGameWorld;

            mGruntKit = new ShipKit_Grunt();
            mTankKit = new ShipKit_Tank();
            mBombKit = new ShipKit_Bomb();
            mStealthKit = new ShipKit_Stealth();
            mLaserKit = new ShipKit_Laser();

            mGruntBossKit = new ShipKit_Grunt_Boss();
            mTankBossKit = new ShipKit_Tank_Boss();
            mBombBossKit = new ShipKit_Bomb_Boss();
        }

        public void StartWave(int CurrentWave)
        {
            mTotalEnemiesSpawned = 0;
            mTimeToNextSpawn = 0.0f;
            mGruntsToSpawn = CalculateGruntsToSpawn(CurrentWave);
            mGruntsSpawned = 0;
            mTanksToSpawn = CalculateTanksToSpawn(CurrentWave);
            mTanksSpawned = 0;
            mBombsToSpawn = CalculateBombsToSpawn(CurrentWave);
            mBombsSpawned = 0;
            mStealthToSpawn = CalculateStealthToSpawn(CurrentWave);
            mStealthSpawned = 0;
            mLasersToSpawn = CalculateLasersToSpawn(CurrentWave);
            mLasersSpawned = 0;

            mGruntBossesSpawned = 0;
            mGruntBossesToSpawn = CalculateGruntBossesToSpawn(CurrentWave);
            mTankBossesSpawned = 0;
            mTankBossesToSpawn = CalculateTankBossesToSpawn(CurrentWave);
            mBombBossesSpawned = 0;
            mBombBossesToSpawn = CalculateBombBossesToSpawn(CurrentWave);

            mTotalEnemies = mGruntsToSpawn + mTanksToSpawn + mBombsToSpawn + mStealthToSpawn + mLasersToSpawn + mGruntBossesToSpawn + mTankBossesToSpawn + mBombBossesToSpawn;
            mWaveLength = Math.Max((float)(2 / Math.Sqrt(CurrentWave)), 0.2f) * mTotalEnemies;
        }

        public int CalculateGruntsToSpawn(int CurrentWave)
        {
            int gruntsToSpawn = 4 * CurrentWave + (CurrentWave * CurrentWave / 10);
            return gruntsToSpawn;
        }

        public int CalculateTanksToSpawn(int CurrentWave)
        {
            int tanksToSpawn = 0;
            if (CurrentWave >= 5)
            {
                tanksToSpawn = CurrentWave / 3;
            }
            return tanksToSpawn;
        }

        public int CalculateBombsToSpawn(int CurrentWave)
        {
            int bombsToSpawn = 0;
            if (CurrentWave % 5 == 0)
            {
                bombsToSpawn = CurrentWave / 5;
            }
            return bombsToSpawn;
        }

        public int CalculateLasersToSpawn(int CurrentWave)
        {
            int lasersToSpawn = 0;
            if (CurrentWave >= 7)
            {
                lasersToSpawn = (int)Math.Sqrt((double)(2 * CurrentWave));
            }
            return lasersToSpawn;
        }

        public int CalculateStealthToSpawn(int CurrentWave)
        {
            int stealthToSpawn = 0;
            if (CurrentWave >= 10)
            {
                int multiplier = (CurrentWave / 20) + 1;
                stealthToSpawn = ((CurrentWave % 10) + 1) * multiplier;
            }
            return stealthToSpawn;
        }

        public int CalculateGruntBossesToSpawn(int CurrentWave)
        {
            int gruntBossesToSpawn = 0;
            if (CurrentWave % 10 == 0 && CurrentWave % 3 == 1)
            {
                gruntBossesToSpawn = ((CurrentWave / 10) + 2) / 3;
            }
            return gruntBossesToSpawn;
        }

        public int CalculateTankBossesToSpawn(int CurrentWave)
        {
            int tankBossesToSpawn = 0;
            if (CurrentWave % 10 == 0 && CurrentWave % 3 == 2)
            {
                tankBossesToSpawn = ((CurrentWave / 10) + 1) / 3;
            }
            return tankBossesToSpawn;
        }

        public int CalculateBombBossesToSpawn(int CurrentWave)
        {
            int bombBossesToSpawn = 0;
            if (CurrentWave % 10 == 0 && CurrentWave % 3 == 0)
            {
                bombBossesToSpawn = ((CurrentWave / 10) + 0) / 3;
            }
            return bombBossesToSpawn;
        }

        public void Update(float pDT)
        {
            mWaveTime += pDT;
            mTimeToNextSpawn -= pDT;

            if (mTimeToNextSpawn <= 0 && mWaveLength > 0)
            {
                mTimeToNextSpawn = (float)mWaveLength / (float)mTotalEnemies;

                int x = 0;
                int y = 0;

                GenerateRandomPosition(ref x, ref y);

                int oldTotalEnemies = mTotalEnemiesSpawned;

                if (mTotalEnemiesSpawned < mTotalEnemies)
                {
                    while (oldTotalEnemies == mTotalEnemiesSpawned)
                    {
                        if (mGruntBossesSpawned < mGruntBossesToSpawn && aaGameWorld.mRand.NextDouble() < 2 * (double)(mGruntBossesToSpawn - mGruntBossesSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnGruntBoss(x, y);
                            mGruntBossesSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                        else if (mTankBossesSpawned < mTankBossesToSpawn && aaGameWorld.mRand.NextDouble() < 2 * (double)(mTankBossesToSpawn - mTankBossesSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnTankBoss(x, y);
                            mTankBossesSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                        else if (mBombBossesSpawned < mBombBossesToSpawn && aaGameWorld.mRand.NextDouble() < 2 * (double)(mBombBossesToSpawn - mBombBossesSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnBombBoss(x, y);
                            mBombBossesSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                        else if (mGruntsSpawned < mGruntsToSpawn && aaGameWorld.mRand.NextDouble() * 2 < (double)(mGruntsToSpawn - mGruntsSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnGrunt(x, y);
                            mGruntsSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                        else if (mTanksSpawned < mTanksToSpawn && aaGameWorld.mRand.NextDouble() < (double)(mTanksToSpawn - mTanksSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnTank(x, y);
                            mTanksSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                        else if (mBombsSpawned < mBombsToSpawn && aaGameWorld.mRand.NextDouble() < (double)(mBombsToSpawn - mBombsSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnBomb(x, y);
                            mBombsSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                        else if (mStealthSpawned < mStealthToSpawn && aaGameWorld.mRand.NextDouble() < (double)(mStealthToSpawn - mStealthSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnStealth(x, y);
                            mStealthSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                        else if (mLasersSpawned < mLasersToSpawn && aaGameWorld.mRand.NextDouble() < (double)(mLasersToSpawn - mLasersSpawned) / (double)(mTotalEnemies - mTotalEnemiesSpawned))
                        {
                            SpawnLaser(x, y);
                            mLasersSpawned++;
                            mTotalEnemiesSpawned++;
                        }
                    }
                }
            }
        }

        public void GenerateRandomPosition(ref int pX, ref int pY)
        {
            int x = 0;
            int y = 0;

            do
            {
                x = aaGameWorld.mRand.Next(0, Constants.REGION_PLAYABLE_WIDTH);
                y = aaGameWorld.mRand.Next(0, Constants.REGION_PLAYABLE_HEIGHT);
            } while (x > (aaGameWorld.aaDisplay.aaCamera.mXPos - 50) && x < (aaGameWorld.aaDisplay.aaCamera.mXPos + 50 + Constants.RESOLUTION_WIDTH) || y > (aaGameWorld.aaDisplay.aaCamera.mYPos - 50) && y < (aaGameWorld.aaDisplay.aaCamera.mYPos + 50 + Constants.RESOLUTION_HEIGHT));

            pX = x;
            pY = y;
        }

        public void SpawnGrunt(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Grunt_Ship", Constants.ENEMY_TEAM, pX, pY, mGruntKit));
        }

        public void SpawnTank(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Tank_Ship", Constants.ENEMY_TEAM, pX, pY, mTankKit));
        }

        public void SpawnBomb(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Bomb_Ship", Constants.ENEMY_TEAM, pX, pY, mBombKit));
        }

        public void SpawnStealth(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Stealth_Ship", Constants.ENEMY_TEAM, pX, pY, mStealthKit));
        }

        public void SpawnLaser(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Laser_Ship", Constants.ENEMY_TEAM, pX, pY, mLaserKit));
        }

        public void SpawnGruntBoss(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Grunt_Boss", Constants.ENEMY_TEAM, pX, pY, mGruntBossKit));
        }

        public void SpawnTankBoss(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Tank_Boss", Constants.ENEMY_TEAM, pX, pY, mTankBossKit));
        }

        public void SpawnBombBoss(int pX, int pY)
        {
            aaGameWorld.mEntityList.Add(new Ship(aaGameWorld, "Bomb_Boss", Constants.ENEMY_TEAM, pX, pY, mBombBossKit));
        }

        public bool DoneSpawning()
        {
            if (mTotalEnemies <= mTotalEnemiesSpawned)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
