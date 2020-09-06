using System.Collections.Generic;
using UnityEngine;

namespace TinyBitTurtle
{
    public class GameCtrl : SingletonMonoBehaviour<GameCtrl>
    {
        public GameSettings settings;
        public GameObject cursor;
        public Actor character;

        protected GameData data;

        private ItemDatabase itemDatabase;
        private List<Enemy> enemyList;
        private Spawner lightSpawner;
        private EnemySpawner enemySpawner;
        private Spawner torchSpawner;
        private Spawner potionSpawner;
        private Spawner chestSpawner;

        private void Start()
        {
            lightSpawner = GameObject.FindGameObjectWithTag("light spawner").GetComponent<Spawner>() as Spawner;
            enemySpawner = GameObject.FindGameObjectWithTag("enemy spawner").GetComponent<EnemySpawner>() as EnemySpawner;
            torchSpawner = GameObject.FindGameObjectWithTag("torch spawner").GetComponent<Spawner>() as Spawner;
            potionSpawner = GameObject.FindGameObjectWithTag("potion spawner").GetComponent<Spawner>() as Spawner;
            chestSpawner = GameObject.FindGameObjectWithTag("chest spawner").GetComponent<Spawner>() as Spawner;

            // place game specific objects
            DungeonModel.Instance.setupObjectsCallback = SetupRoomObjects;
        }

        public void Init()
        {
            // default character class
            //player = CharacterFactory.Instance.Create(CharacterType.fighter);
           character.Init();

            // fill the pool
            enemySpawner.Init();
        }

        public void Setup()
        {
            // default character class
            character.Setup();

            // cache the player level
            //int playerLevel = player.GetLevel();

            //GetRandomCreature(playerLevel);

            //Actor newEnemy = CharacterFactory.Instance.Create(CharacterType.skeleton);
            //newEnemy.Init(playerLevel);

            //enemyList.Add(newEnemy);
        }

        public void EndOfSpawn(GameObject FX)
        {
            FX.SetActive(false);
        }

        private void SetupLights(DungeonSettings gameInit, int level, Vector2Int Pos, Vector2Int Dim)
        {
            int NumOfLights = settings.objectSet[level <= settings.objectSet.Length ? level : 0].lightsPerRoom.Random;
            for (int i = 0; i < NumOfLights; ++i)
            {
                float startXPos = Random.Range(Pos.x, Pos.x + Dim.x);
                float startYPos = Random.Range(Pos.y, Pos.y + Dim.y);

                Vector3 pos = new Vector3(startXPos, startYPos, lightSpawner.transform.position.z);
                lightSpawner.spawn(null, pos - lightSpawner.transform.position);
            }
        }

        private void SetupEnemies(DungeonSettings gameInit, int level, Vector2Int Pos, Vector2Int Dim)
        {
            int NumOfEnemies = settings.enemiesSets[level <= settings.enemiesSets.Length ? level : 0].enemiesPerRoom.Random;
            for (int i = 0; i < NumOfEnemies; ++i)
            {
                float startXPos = Random.Range(Pos.x, Pos.x + Dim.x);
                float startYPos = Random.Range(Pos.y, Pos.y + Dim.y);

                Vector3 pos = new Vector3(startXPos, startYPos, 0.0f);
                Vector3 localPos = pos - enemySpawner.transform.position;
                enemySpawner.spawn(null, localPos);
            }
        }

        private void SetupTorches(DungeonSettings gameInit, int level, Vector2Int Pos, Vector2Int Dim)
        {
            int NumOfTorches = Random.Range(1, settings.objectSet[level <= settings.objectSet.Length ? level : 0].torchesPerRoom);
            for (int i = 0; i < NumOfTorches; ++i)
            {
                int whichWall = Random.Range(0, 4);
                float startXPos = 0;
                float startYPos = 0;
                float padding = 0.3f;
                float lastTorchNorth = 0;
                float spacing = 2;

                switch (whichWall)
                {
                    // north
                    case 0:
                        startXPos = Mathf.Clamp(lastTorchNorth + Random.Range(spacing, Dim.x - spacing), Pos.x, Pos.x + Dim.x);
                        startYPos = Pos.y + Dim.y - padding;
                        lastTorchNorth = startXPos;
                        break;
                    // east
                    case 1:
                        startXPos = Pos.x + Dim.x - padding;
                        startYPos = Random.Range(Pos.y, Pos.y + Dim.y);
                        break;

                    // south
                    case 2:
                        startXPos = Random.Range(Pos.x, Pos.x + Dim.x);
                        startYPos = Pos.y + padding;
                        break;

                    // west
                    case 3:
                        startXPos = Pos.x + padding;
                        startYPos = Random.Range(Pos.y, Pos.y + Dim.y);
                        break;

                }

                Vector3 pos = new Vector3(startXPos, startYPos, 0.0f);
                torchSpawner.spawn(null, pos - torchSpawner.transform.position);
            }
        }

        private void SetupPotions(DungeonSettings gameInit, int level, Vector2Int Pos, Vector2Int Dim)
        {
            int NumOfPotions = Random.Range(1, settings.objectSet[level <= settings.objectSet.Length ? level : 0].potionPerRoom);
            for (int i = 0; i < NumOfPotions; ++i)
            {
                float startXPos = Random.Range(Pos.x, Pos.x + Dim.x);
                float startYPos = Random.Range(Pos.y, Pos.y + Dim.y);

                Vector3 pos = new Vector3(startXPos, startYPos, 0.0f);
                potionSpawner.spawn(null, pos - potionSpawner.transform.position);
            }
        }

        private void SetupChests(DungeonSettings gameInit, int level, Vector2Int Pos, Vector2Int Dim)
        {
            int NumOfEnemies = Random.Range(1, settings.objectSet[level <= settings.objectSet.Length ? level : 0].chestPerRoom);
            for (int i = 0; i < NumOfEnemies; ++i)
            {
                float startXPos = Random.Range(Pos.x, Pos.x + Dim.x);
                float startYPos = Random.Range(Pos.y, Pos.y + Dim.y);

                Vector3 pos = new Vector3(startXPos, startYPos, 0.0f);
                chestSpawner.spawn(null, pos - chestSpawner.transform.position);
            }
        }

        public void SetupRoomObjects(DungeonSettings gameInit, int level, Vector2Int pos, Vector2Int dim, Corridor corridor)
        {
            SetupLights(gameInit, level, pos, dim);
            SetupEnemies(gameInit, level, pos, dim);
            SetupTorches(gameInit, level, pos, dim);
            SetupPotions(gameInit, level, pos, dim);
            SetupChests(gameInit, level, pos, dim);
        }

        public void onShowStats(ActorCtrl characterCtrl)
        {
        }

        public void onSelect(Actor character)
        {
            // if user
            ShowAggroRange(character);

            //ShowStatsPanel();
        }

        public void onAttack(Actor character)
        {
            // characterCtrl.GetHit(dmgAmount)
        }

        public void onOpen(Actor character)
        {
            //characterCtrl.Open(chest instance)
        }

        public void onMove(Actor character)
        {
            if(character == null)
                return;

            character.onMove(character);

            // check proximity of pickables
            //ProximityCtrl.Instance.isInRange(characterCtrl.transform.position);
        }

        public void onTogleAltMode(Actor character)
        {
            // flip the mode
            character.mode = (Mode)((int)(++character.mode) % (int)(Mode.Max));

            // use diferrent settings based on mode
            string className = character.mode != Mode.Normal ? "WispSettings" : character.characterSettingsNormalModeName;

            // the actual switching
            character.onSwitchCharacter(className);
        }

        private void ShowAggroRange(Actor character)
        {
            //characterCtrl.aggroRange;
        }

        private void ShowStatsPanel()
        {
        }
    }
}
