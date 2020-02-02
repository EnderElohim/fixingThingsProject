using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scrGameManager : MonoBehaviour
{
    public bool bussy = false;
    public scrImagaDatabase database;
    public GameObject ImagePanel;
    public Image mainImage;
    public Image ending;
    public Image successEnding;

    public int currentLevel = 0;

    public GameObject[] failAnimtions;
    [SerializeField]
    public List<fam> fixAnims = new List<fam>();
    public List<effectLibrary> specialEffects = new List<effectLibrary>();


    private int currentSafetyLevel;
    private Dictionary<int, GameObject> currentProblems = new Dictionary<int, GameObject>();


    public GameObject currentAnimationObject;
    public Sprite level2ex1, level2ex2;
    public Sprite level3ex1;
    public int lastClickedTarget = -1;


    private void Start()
    {
        HandleLevel();
    }
    public void RestartLevel()
    {
        HandleLevel();
    }
    public void NextLevel()
    {
        if (currentLevel < database.maxLevel()-1)
        {
            
            currentLevel++;
            HandleLevel();
            
        }
    }

    private void CreateProblemImage(ImageType[] _problems)
    {
        List<GameObject> newImages = new List<GameObject>();
        for (int i = 0; i < _problems.Length; i++)
        {
            Image img = Instantiate(mainImage);
            img.sprite = _problems[i].avatar;
            currentProblems.Add(_problems[i].target, img.gameObject);
            
            //img.transform.position = Vector3.zero;

            newImages.Add(img.gameObject);

        }
        for (int i = 0; i < newImages.Count; i++)
        {
            //newImages[i].transform.parent = mainImage.transform;
            newImages[i].transform.SetParent(mainImage.transform,false);
        }
        
    }
    



    private void HandleLevel()
    {
        currentSafetyLevel = 0;
        currentProblems.Clear();
        ending.enabled = false;
        successEnding.gameObject.SetActive(false);
        //clear fail anims
        for (int i = 0; i < failAnimtions.Length; i++)
        {
            failAnimtions[i].SetActive(false);
        }


        //Clear old buttons
        for (int i = 0; i < ImagePanel.transform.childCount; i++)
        {
            Destroy(ImagePanel.transform.GetChild(i).gameObject);
        }
        //clear problems
        for (int i = 0; i < mainImage.transform.childCount; i++)
        {
            Destroy(mainImage.transform.GetChild(i).gameObject);
        }

        //Bring base image
        ChangeMainPicutre(database.CurrentMainImage(currentLevel));

        //Create Problems
        ImageType[] problems = database.CurrentProblems(currentLevel);
        CreateProblemImage(problems);

        if (currentLevel == 2)
        {
            //Create dangerous buttons
            ImageType[] dangerous = database.CurrentDangerousButtons(currentLevel);
            for (int i = 0; i < dangerous.Length; i++)
            {
                CreateDangerousButton(dangerous[i].avatar, dangerous[i].target);
            }
            //Create safety buttons
            ImageType[] safety = database.CurrentSafetyButtons(currentLevel);
            currentSafetyLevel = safety.Length;
            for (int i = 0; i < safety.Length; i++)
            {
                CreateSafetyButton(safety[i].avatar, safety[i].target);
            }

            
        }
        else
        {
            //Create safety buttons
            ImageType[] safety = database.CurrentSafetyButtons(currentLevel);
            currentSafetyLevel = safety.Length;
            for (int i = 0; i < safety.Length; i++)
            {
                CreateSafetyButton(safety[i].avatar, safety[i].target);
            }

            //Create dangerous buttons
            ImageType[] dangerous = database.CurrentDangerousButtons(currentLevel);
            for (int i = 0; i < dangerous.Length; i++)
            {
                CreateDangerousButton(dangerous[i].avatar, dangerous[i].target);
            }
        }

        

        //Create normal buttons;
        ImageType[] normal = database.CurrentNormalButtons(currentLevel);
        for (int i = 0; i < normal.Length; i++)
        {
            CreateNormalButton(normal[i].avatar, normal[i].target);
        }
    }
    private void ChangeMainPicutre(Sprite _spr)
    {
        mainImage.sprite = _spr;
    }
    private void CreateSafetyButton(Sprite _spr, int _target)
    {
        GameObject NewObj = new GameObject(); 
        Image NewImage = NewObj.AddComponent<Image>(); 
        NewImage.sprite = _spr; 
        NewObj.GetComponent<RectTransform>().SetParent(ImagePanel.transform);
        NewObj.AddComponent<Button>();
        NewObj.GetComponent<Button>().onClick.AddListener(delegate { SafetyButton(_target); });
        RectTransform rt = NewObj.GetComponent<RectTransform>();
        rt.localScale = Vector3.one;
        Vector3 pos = rt.localPosition;
        pos.z = 0;
        rt.localPosition = pos;
        rt.localScale = Vector3.one;
        rt.localRotation = Quaternion.Euler(0, 0, 0);
        NewObj.SetActive(true);
    }
    private void CreateDangerousButton(Sprite _spr, int _target)
    {
        GameObject NewObj = new GameObject(); 
        Image NewImage = NewObj.AddComponent<Image>(); 
        NewImage.sprite = _spr; 
        NewObj.GetComponent<RectTransform>().SetParent(ImagePanel.transform); 
        NewObj.AddComponent<Button>();
        NewObj.GetComponent<Button>().onClick.AddListener(delegate { DangerousButton(_target); });
        RectTransform rt = NewObj.GetComponent<RectTransform>();
        rt.localScale = Vector3.one;
        Vector3 pos = rt.localPosition;
        pos.z = 0;
        rt.localPosition = pos;
        rt.localScale = Vector3.one;
        rt.localRotation = Quaternion.Euler(0, 0, 0);
        NewObj.SetActive(true);

    }
    private void CreateNormalButton(Sprite _spr, int _target)
    {
        GameObject NewObj = new GameObject(); 
        Image NewImage = NewObj.AddComponent<Image>(); 
        NewImage.sprite = _spr; 
        NewObj.GetComponent<RectTransform>().SetParent(ImagePanel.transform); 
        NewObj.AddComponent<Button>();
        NewObj.GetComponent<Button>().onClick.AddListener(delegate { NormalButton(_target); });
        RectTransform rt = NewObj.GetComponent<RectTransform>();
        rt.localScale = Vector3.one;
        Vector3 pos = rt.localPosition;
        pos.z = 0;
        rt.localPosition = pos;
        rt.localScale = Vector3.one;
        rt.localRotation = Quaternion.Euler(0, 0, 0);   
        NewObj.SetActive(true); 
        
    }
    private void DangerousButton(int _target)
    {

        if (bussy)
        {
            
            return;
        }
            
        
        if (currentSafetyLevel > 0)
        {
            Sprite spr = database.CurrentFailState(currentLevel);
            ending.sprite = spr;
            ending.enabled = true;
            //clear problems
            for (int i = 0; i < mainImage.transform.childCount; i++)
            {
                Destroy(mainImage.transform.GetChild(i).gameObject);
            }
            if(failAnimtions[currentLevel])
                failAnimtions[currentLevel].SetActive(true);
            




        }
        else
        {
            ClearProblem(_target);
        }
    }

    private void SafetyButton(int _target)
    {
        if (bussy)
        {
           
            return;
        }
        
        currentSafetyLevel -= 1;
        ClearProblem(_target);
    }
    
    private void NormalButton(int _target)
    {
        if (bussy)
        {
            
            return;
        }
        
        ClearProblem(_target);
    }

    private void ClearProblem(int _target)
    {
        
        if (currentProblems.ContainsKey(_target))
        {
            print("a");
            for (int i = 0; i < fixAnims.Count; i++)
            {
                print("b");
                if (fixAnims[i].level == currentLevel)
                {
                    print("c");
                    if (fixAnims[i].target == _target)
                    {
                        print("d");
                        float dur = fixAnims[i].duration;
                        fixAnims[i].anx.gameObject.SetActive(true);
                        if (currentLevel == 1 && _target == 0)
                        {
                            lastClickedTarget = _target;
                            
                            
                        }
                        else if (currentLevel == 1 && _target == 2)
                        {
                            lastClickedTarget = _target;
                            
                        }
                        else if(currentLevel == 2 && _target == 2)
                        {
                            
                            lastClickedTarget = _target;
                        }
                        else
                        {
                            print(currentLevel + ":" + _target);
                            Destroy(currentProblems[_target].gameObject, dur);
                            currentProblems.Remove(_target);
                        }
                        Debug.Log("PLAY ANIMATIONS");
                        for (int y = 0; y < specialEffects.Count; y++)
                        {
                            if(specialEffects[y].level == currentLevel && specialEffects[y].target == _target)
                            {
                                foreach (ParticleSystem item in specialEffects[y].effects)
                                {
                                    item.Play();
                                }
                            }
                        }
                        bussy = true;
                        currentAnimationObject = fixAnims[i].anx.gameObject;
                        Invoke("DisabeAfterTime", dur);
                        
                        Invoke("ControlEnding", dur);
                    }
                }

            }
        }
        else
        {
            Debug.LogWarning(_target + " is not exist");
            
        }
    }
    private void ControlEnding()
    {
        if (currentProblems.Count <= 0)
        {
            Sprite spr = database.CurrentCompletedState(currentLevel);
            successEnding.sprite = spr;
            successEnding.gameObject.SetActive(true);
            print("Game over");
        }
    }
    
    private void DisabeAfterTime()
    {
        bussy = false;
        currentAnimationObject.SetActive(false);
        if (currentLevel == 1)
        {
            if(lastClickedTarget == 0)
            {
                if (currentProblems.ContainsKey(lastClickedTarget))
                {
                    currentProblems[lastClickedTarget].gameObject.GetComponent<Image>().sprite = level2ex1;

                }
                currentProblems.Remove(lastClickedTarget);
            }
            if(lastClickedTarget == 2)
            {
                if (currentProblems.ContainsKey(lastClickedTarget))
                {
                    currentProblems[lastClickedTarget].gameObject.GetComponent<Image>().sprite = level2ex2;
                }
                
                currentProblems.Remove(lastClickedTarget);
            }
        }
        if(currentLevel == 2)
        {
            if (lastClickedTarget == 2)
            {
                if (currentProblems.ContainsKey(lastClickedTarget))
                {
                    currentProblems[lastClickedTarget].gameObject.GetComponent<Image>().sprite = level3ex1;

                }
                currentProblems.Remove(lastClickedTarget);
            }
        }
        
    }
    private void Level2Exception()
    {
        
        currentProblems[lastClickedTarget].gameObject.GetComponent<Image>().sprite = level2ex1;

    }
    



}
[System.Serializable]
public struct fam
{
    public GameObject anx;
    public int target;
    public float duration;
    public int level;
}

[System.Serializable]
public struct effectLibrary
{
    public ParticleSystem[] effects;
    public int level;
    public int target;




}