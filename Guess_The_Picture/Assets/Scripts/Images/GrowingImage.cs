using UnityEngine;
using UnityEngine.UIElements;

public class GrowingImage : MonoBehaviour
{
    Vector2 position;
    float maxWidthPosition;
    float maxHeightPosition;
    [SerializeField] float defaultScale;
    [SerializeField] float maxScale;

    float scaleTime = 0;
    float chrono = 15;

    public float Chrono {get => chrono; set => chrono = value; }
    public float ScaleTime {get => scaleTime; private set => scaleTime = value; }

    private void Start()
    {
        SetCircleDefaultParams();
    }

    private void SetCircleDefaultParams()
    {
        gameObject.transform.localScale = new Vector3(defaultScale, defaultScale, 1);
        SetRandomPosition();
    }

    //Set a random Position in mawWidthPos and maxHeightPos for the circle
    public void SetRandomPosition()
    {
        maxWidthPosition = Random.Range(-200, 200);
        maxHeightPosition = Random.Range(200, 600);
        position = new Vector2(maxWidthPosition, maxHeightPosition);
        gameObject.transform.localPosition = position;
    }

    //Reset the crop 
    public void ResetScaleTime()
    {
        scaleTime = 0;
    }

    //Update of the growing crop
    public void CircleGrowthUpdate(float _chronoTimer)
    {
        scaleTime += Time.deltaTime;
        if (_chronoTimer > 0)
        {
            //Debug.Log("[CircleGrowthUpdate::GrowingImage] => time " + time);
            //Debug.Log("[CircleGrowthUpdate::GrowingImage] => defaultScale " + defaultScale);
            //Debug.Log("[CircleGrowthUpdate::GrowingImage] => MaxScale " + maxScale);
            //Debug.Log("[CircleGrowthUpdate::GrowingImage] => Lerp " +  Mathf.Lerp(defaultScale, maxScale, time/chrono));
            float _actualScale = Mathf.Lerp(defaultScale, maxScale, scaleTime /chrono);
            //Debug.Log("[CircleGrowthUpdate::GrowingImage] => actual Scale = " + _actualScale);
            gameObject.transform.localScale = new Vector3 (_actualScale,_actualScale,1);
            //Debug.Log("[CircleGrowthUpdate::GrowingImage] => " + gameObject.transform.localScale.x);
        }
    }
}
