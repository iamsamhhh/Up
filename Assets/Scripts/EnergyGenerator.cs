using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject energyPrefeb, bombPrefeb, coinPrefeb;
    [SerializeField]
    Transform player;
    [SerializeField]
    Vector2 widthAndHeight;
    [SerializeField]
    float despawnDistance;
    [SerializeField]
    int maxNumOfEnergy, numOfBombAddPerLv, maxNumOfCoin, noSpawningDistance;
    int numOfBomb;
    int supposedNumOfEnergy;
    [SerializeField]
    Vector2 scrWidthAndHeight;

    List<Transform> energyList = new List<Transform>();
    List<Transform> bombList = new List<Transform>();
    List<Transform> coinList = new List<Transform>();
    List<Transform> energyToBeDeleted = new List<Transform>();

    void Awake()
    {
        supposedNumOfEnergy = maxNumOfEnergy;
        numOfBomb = (UserData.defaultUserData.gameLevel-1)*numOfBombAddPerLv;
        supposedNumOfEnergy -= numOfBomb;
        if (supposedNumOfEnergy < 0){
            supposedNumOfEnergy = 0;
        }
        for (int i = 0; i < supposedNumOfEnergy; i++){
            var randx = Random.Range(player.position.x - widthAndHeight.x/2, player.position.x + widthAndHeight.x/2);
            var randy = Random.Range(player.position.y - widthAndHeight.y/2, player.position.y + widthAndHeight.y/2);
            energyList.Add(Instantiate(energyPrefeb, new Vector2(randx, randy), Quaternion.identity).transform);
        }

        for (int i = 0; i < maxNumOfCoin; i++){
            var randx = Random.Range(player.position.x - widthAndHeight.x/2, player.position.x + widthAndHeight.x/2);
            var randy = Random.Range(player.position.y - widthAndHeight.y/2, player.position.y + widthAndHeight.y/2);
            coinList.Add(Instantiate(coinPrefeb, new Vector2(randx, randy), Quaternion.identity).transform);
        }

        for (int i = 0; i < numOfBomb; i++){
            var randx = Random.Range(player.position.x - widthAndHeight.x/2, player.position.x + widthAndHeight.x/2);
            var randy = Random.Range(player.position.y - widthAndHeight.y/2, player.position.y + widthAndHeight.y/2);
            var bomb = Instantiate(bombPrefeb, new Vector2(randx, randy), Quaternion.identity).transform;
            bomb.gameObject.SetActive(false);
            bombList.Add(bomb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var energy in energyList)
        {
            // Check if the energy is too far away
            if((player.position - energy.position).magnitude > despawnDistance){
                Recycle(energy, EObjectType.Energy);
            }
            else if (energy.position.y <= 3){
                energy.gameObject.SetActive(false);
            }
        }
        foreach (var bomb in bombList){
            if((player.position - bomb.position).magnitude > despawnDistance){
                Recycle(bomb, EObjectType.Bomb);
            }
            else if (bomb.position.y <= 3){
                bomb.gameObject.SetActive(false);
            }
        }
        foreach (var coin in coinList){
            if((player.position - coin.position).magnitude > despawnDistance){
                Recycle(coin, EObjectType.Coin);
            }
            else if (coin.position.y <= 3){
                coin.gameObject.SetActive(false);
            }
        }

        foreach(var energy in energyToBeDeleted){
            energyList.Remove(energy);
            Destroy(energy.gameObject);
        }

        energyToBeDeleted.Clear();
    }

    public void LevelUp(){
        var newNumOfBomb = (UserData.defaultUserData.gameLevel-1)*numOfBombAddPerLv;
        var numOfBombDiff = newNumOfBomb - numOfBomb;
        supposedNumOfEnergy = maxNumOfEnergy - newNumOfBomb;
        for (int i = 0; i < numOfBombDiff; i++){
            var bomb = Instantiate(bombPrefeb);
            bombList.Add(bomb.transform);
            RemoveObject(bomb.transform, EObjectType.Bomb);
        }
        numOfBomb = newNumOfBomb;
    }

    /// <summary>
    /// Activate when object are out of range
    /// </summary>
    /// <param name="objectTrans"></param>
    private void Recycle(Transform objectTrans, EObjectType type){
        switch(type){
            case EObjectType.Energy:
                if (energyList.Count > supposedNumOfEnergy){
                    energyToBeDeleted.Add(objectTrans);
                    return;
                }
                break;
            case EObjectType.Bomb: 
                break;
        }

        var spawnDirection = (player.position - objectTrans.position).normalized;
        
        objectTrans.gameObject.SetActive(true);
        // var randx = Random.Range(player.position.x - widthAndHeight.x/2, player.position.x + widthAndHeight.x/2);
        // var randy = Random.Range(player.position.y - widthAndHeight.y/2, player.position.y + widthAndHeight.y/2);
        // if (!SpawnInSight(randx, randy)){
        //     objectTrans.position = new Vector2(randx, randy);
        // }
        objectTrans.position = player.position + spawnDirection * (despawnDistance-Random.Range(1,10));
    }

    /// <summary>
    /// Activate when object are hit
    /// </summary>
    /// <param name="ObjectTrans"></param>
    public void RemoveObject(Transform objectTrans, EObjectType type){
        switch(type){
            case EObjectType.Energy:
                if (energyList.Count > supposedNumOfEnergy){
                    energyToBeDeleted.Add(objectTrans);
                    return;
                }
                break;
            case EObjectType.Bomb: 
                break;
        }
        var randx = Random.Range(player.position.x - widthAndHeight.x/2, player.position.x + widthAndHeight.x/2);
        var randy = Random.Range(player.position.y - widthAndHeight.y/2, player.position.y + widthAndHeight.y/2);
        if (!SpawnInSight(randx, randy)){
            objectTrans.position = new Vector2(randx, randy);
        }
        else{
            RemoveObject(objectTrans, type);
        }
    }

    /// <summary>
    /// Check if cordinate x and y are near player
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>Return true if cordinate are near player</returns>
    bool SpawnInSight(float x, float y){
        
        return (player.position - new Vector3(x, y, 0)).magnitude < noSpawningDistance;
    }

}
