using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    Vector2 scrWidthAndHeight;

    List<Transform> energyList = new List<Transform>();
    List<Transform> bombList = new List<Transform>();
    List<Transform> coinList = new List<Transform>();

    void Awake()
    {
        numOfBomb = (GameManager.instance.gameLevel-1)*numOfBombAddPerLv;
        maxNumOfEnergy -= numOfBomb;
        if (maxNumOfEnergy < 0){
            maxNumOfEnergy = 0;
        }
        for (int i = 0; i < maxNumOfEnergy; i++){
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
                Recycle(energy);
            }
            else if (energy.position.y <= 3){
                energy.gameObject.SetActive(false);
            }
        }
        foreach (var bomb in bombList){
            if((player.position - bomb.position).magnitude > despawnDistance){
                Recycle(bomb);
            }
            else if (bomb.position.y <= 3){
                bomb.gameObject.SetActive(false);
            }
        }
        foreach (var coin in coinList){
            if((player.position - coin.position).magnitude > despawnDistance){
                Recycle(coin);
            }
            else if (coin.position.y <= 3){
                coin.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Activate when object are out of range
    /// </summary>
    /// <param name="objectTrans"></param>
    private void Recycle(Transform objectTrans){
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
    public void RemoveObject(Transform ObjectTrans){
        var randx = Random.Range(player.position.x - widthAndHeight.x/2, player.position.x + widthAndHeight.x/2);
        var randy = Random.Range(player.position.y - widthAndHeight.y/2, player.position.y + widthAndHeight.y/2);
        if (!SpawnInSight(randx, randy)){
            ObjectTrans.position = new Vector2(randx, randy);
        }
        else{
            RemoveObject(ObjectTrans);
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
