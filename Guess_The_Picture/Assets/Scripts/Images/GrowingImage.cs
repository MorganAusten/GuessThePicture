using TMPro;
using UnityEngine;
using ImageUI = UnityEngine.UI.Image;

public class GrowingImage : MonoBehaviour
{
    Vector2 position;
    [SerializeField] Color defaultColorImage;
    [SerializeField] Color defaultColorText;

    [SerializeField] ImageUI cropImage;
    [SerializeField] float defaultScale;
    [SerializeField] float maxScale;
    [SerializeField] TMP_Text imageTitle;

    float maxWidthPosition;
    float maxHeightPosition;

    float cropOpacityScale = 1;
    float titleOpacityScale = 0;
    float scaleTime = 0;
    float chrono = 15;

    bool titleAppeared = false;
    public bool TitleAppeared { get => titleAppeared; set => titleAppeared = value; } 
    public TMP_Text ImageTitle { get { return imageTitle; } set { imageTitle = value; } }
    public float Chrono { get => chrono; set => chrono = value; }
    public float ScaleTime { get => scaleTime; private set => scaleTime = value; }

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
        maxWidthPosition = Random.Range(-300, 300);
        maxHeightPosition = Random.Range(200, 600);
        position = new Vector2(maxWidthPosition, maxHeightPosition);
        gameObject.transform.localPosition = position;
    }

    //Reset the crop 
    public void ResetScaleTime()
    {
        scaleTime = 0;
    }

    public void TurnCropOpacity(bool _bool)
    {
        if (!_bool)
        {
            Color _newColor = cropImage.color;
            if (cropOpacityScale > 0)
            {
                cropOpacityScale -= Time.deltaTime *0.5f;
                float _actualScale = Mathf.Lerp(0, 255, cropOpacityScale /1);
                _newColor.a = _actualScale/255;
                cropImage.color = _newColor;
            }
            else
                TurnTitleOpacity(true);
                
        }
        else
        {
            cropImage.color = defaultColorImage;
            cropOpacityScale = 1;
        }
    }

    public void TurnTitleOpacity(bool _bool)
    {
        if (_bool)
        {
            Color _newColor = imageTitle.color;
            if (titleOpacityScale < 1)
            {
                titleOpacityScale += Time.deltaTime *0.5f;
                float _actualScale = Mathf.Lerp(0, 255, titleOpacityScale /1);
                _newColor.a = _actualScale/255;
            }
            else
                titleAppeared = true;

            imageTitle.color = _newColor;
        }
        else
        {
            imageTitle.color = defaultColorText;
            titleOpacityScale = 0;
        }
    }

    //Update of the growing crop
    public void CircleGrowthUpdate(float _chronoTimer)
    {
        scaleTime += Time.deltaTime;
        if (_chronoTimer > 0)
        {
            float _actualScale = Mathf.Lerp(defaultScale, maxScale, scaleTime /chrono);
            gameObject.transform.localScale = new Vector3(_actualScale, _actualScale, 1);
        }
    }
}
