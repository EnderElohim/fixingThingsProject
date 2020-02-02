using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scrImagaDatabase : MonoBehaviour
{
    [SerializeField]
    public List<ImageObject> db = new List<ImageObject>();




    public int maxLevel()
    {
        return db.Count;
    }
    public ImageType[] CurrentSafetyButtons(int lvl)
    {
        return db[lvl].safety;
    }
    public ImageType[] CurrentDangerousButtons(int lvl)
    {
        return db[lvl].dangerous;
    }
    public ImageType[] CurrentNormalButtons(int lvl)
    {
        return db[lvl].normal;
    }
    public Sprite CurrentFailState(int lvl)
    {
        return db[lvl].failed;
    }
    public Sprite CurrentCompletedState(int lvl) 
    {
        return db[lvl].completed;
    }
    public Sprite CurrentMainImage(int lvl)
    {
        return db[lvl].mainImage;
    }
    

    public ImageType[] CurrentProblems(int lvl)
    {
        return db[lvl].problems;
    }







}
[System.Serializable]
public class ImageObject
{
    public Sprite mainImage;
    public ImageType[] problems;
    public ImageType[] safety;
    public ImageType[] dangerous;
    public ImageType[] normal;
    public Sprite completed;
    public Sprite failed;   
    


}
[System.Serializable]
public class ImageType
{
    public Sprite avatar;
    public int target;
    
}
