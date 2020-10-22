using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Cauldron : Station
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.setStationType(StationType.Cauldron);
        base.idleMat = idleMat;
        base.cookMat = cookMat;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        base.Update();
    }
    */
    /*
    void Cook() {
        if (base.storedItem)
        {
            if (!isCooking)
            {
                isCooking = true;
                base.canPickup = false;
                base.locked = true;

                // change mat / shader
                r.material = cookMat;

                // set cooking time
                totalCooktime = base.cooktimeMap[new Tuple<StationType, IngredientType>(station, IngredientType.Bone)]; //todo replace with ingredient type of stored object rather than just using bone
                timeUntilComplete = totalCooktime;

                /*
                //***************************************************************************
                //UI LMAO
                //create canvas
                GameObject progressCanvas = new GameObject();
                progressCanvas.AddComponent(typeof(Canvas));
                progressCanvas.AddComponent(typeof(GraphicRaycaster));
                progressCanvas.AddComponent(typeof(CanvasScaler));
                Canvas curCanvas = progressCanvas.GetComponent(typeof(Canvas)) as Canvas;
                curCanvas.renderMode = RenderMode.WorldSpace;
                
                progressCanvas.transform.parent = transform;
                progressCanvas.transform.position = transform.position;

                //create progressbar object
                GameObject progressBar = new GameObject();
                progressBar.transform.parent = progressCanvas.transform;
                progressBar.AddComponent(typeof(Slider));
                bar = progressBar.GetComponent(typeof(Slider)) as Slider;
                bar.value = (float)timeUntilComplete / totalCooktime;
                print(bar.value);

                //create rectangle area object
                GameObject fillArea = new GameObject();
                fillArea.transform.parent = progressBar.transform;
                fillArea.AddComponent(typeof(RectTransform));
                RectTransform rect = fillArea.GetComponent(typeof(RectTransform)) as RectTransform;
                rect.anchorMin = new Vector2(0, 0.25f);
                rect.anchorMax = new Vector2(1, 0.75f);
                rect.offsetMin = new Vector2(0,0);
                rect.offsetMax = new Vector2(0,0);
                bar.fillRect = rect;

                //image/colour for filled area
                GameObject fillAreaImage = new GameObject();
                fillAreaImage.transform.parent = fillArea.transform;
                fillAreaImage.AddComponent(typeof(Image));
                Image image1 = fillAreaImage.GetComponent(typeof(Image)) as Image;
                image1.color = new Color(1, 0.5f, 0, 1);
                fillAreaImage.AddComponent(typeof(RectTransform));

                RectTransform rect2 = fillAreaImage.GetComponent(typeof(RectTransform)) as RectTransform;
                //no anchors
                rect2.offsetMin = new Vector2(0, 0);
                rect2.offsetMax = new Vector2(0, 0);
                rect2.sizeDelta = new Vector2(100, 100);

                //background image/colour
                GameObject background = new GameObject();
                background.transform.parent = progressBar.transform;
                background.AddComponent(typeof(Image));
                Image image2 = background.GetComponent(typeof(Image)) as Image;
                image2.color = new Color(0, 1, 0.5f, 1);

                //background rect
                background.AddComponent(typeof(RectTransform));
                RectTransform rect3 = background.GetComponent(typeof(RectTransform)) as RectTransform;
                rect3.anchorMin = new Vector2(0, 0.25f);
                rect3.anchorMax = new Vector2(1, 0.75f);

                rect3.offsetMin = new Vector2(0, 0);
                rect3.offsetMax = new Vector2(0, 0);

                //***************************************************************************

                print("creation done");
                
            }
            else
            {
                timeUntilComplete -= 1;
                print(bar.value);
                bar.value = ((float)timeUntilComplete) / totalCooktime;
                if (timeUntilComplete <= 0)
                {
                    isCooking = false;
                    base.canPickup = true;
                    r.material = idleMat;

                    //todo add sound effect or something on completion

                    //todo get ingredient type component from stored object, use in ingredient constructor to make new ingredient
                    IngredientType outputType = base.ingredientMap[new Tuple<StationType, IngredientType>(station, IngredientType.Bone)];
                    print("output:");
                    print(outputType);
                    print("end output");
                    //todo GameObject processedOutput = Instantiate(Ingredient(outputType), base.storedItem.transform.position, base.storedItem.transform.rotation);
                    Destroy(base.storedItem);
                    base.storedItem = null;
                    //base.storedItem = processedOutput;

                    //destroy progress bar
                    Destroy(progressCanvas);
                    progressCanvas = null; 
                    progressBar = null;
                    bar = null;
                }
            }
        }
    }
    */
}
