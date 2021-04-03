using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Controler : MonoBehaviour
{
    List<Button> ListBtn = new List<Button>();
    public Sprite background_image;
    public Transform Panel;
    [SerializeField]
    private GameObject Button;
    GameObject btn;
    [SerializeField]
    public Sprite[] sourceSprites;

    List<GameObject> lstBtnCreate = new List<GameObject>();

    private bool firstButtonClick, secondButtonClick;
    string firstButtonName, secondButtonName;
    int firstIndex, secondIndex, numMatch, numWin;

    public List<Sprite> sprites = new List<Sprite>();

    List<ManChoi> lstManChoi = new List<ManChoi>();

    int ngang;
    int doc;

    int ManHienTai;
   


    private void Awake()
    {
        #region thêm màn
        lstManChoi.Add(new ManChoi() { MaMan = 1, TenMan = "2x2 dễ", Ngang = 2, Doc = 2, ThoiGian = 30, MaCapDo = 1 });
        lstManChoi.Add(new ManChoi() { MaMan = 2, TenMan = "2x3 dễ", Ngang = 2, Doc = 3, ThoiGian = 35, MaCapDo = 1 });
        lstManChoi.Add(new ManChoi() { MaMan = 3, TenMan = "2x4 dễ", Ngang = 2, Doc = 4, ThoiGian = 40, MaCapDo = 1 });
        lstManChoi.Add(new ManChoi() { MaMan = 4, TenMan = "2x5 dễ", Ngang = 2, Doc = 5, ThoiGian = 45, MaCapDo = 1 });
        lstManChoi.Add(new ManChoi() { MaMan = 5, TenMan = "3x2 dễ", Ngang = 3, Doc = 2, ThoiGian = 35, MaCapDo = 1 });
        lstManChoi.Add(new ManChoi() { MaMan = 6, TenMan = "3x3 dễ", Ngang = 3, Doc = 3, ThoiGian = 40, MaCapDo = 1 });
        lstManChoi.Add(new ManChoi() { MaMan = 7, TenMan = "3x4 dễ", Ngang = 3, Doc = 4, ThoiGian = 45, MaCapDo = 1 });
        lstManChoi.Add(new ManChoi() { MaMan = 8, TenMan = "3x5 dễ", Ngang = 3, Doc = 5, ThoiGian = 50, MaCapDo = 1 });

        lstManChoi.Add(new ManChoi() { MaMan = 9, TenMan = "2x2 trung bình", Ngang = 2, Doc = 2, ThoiGian = 25, MaCapDo = 2 });
        lstManChoi.Add(new ManChoi() { MaMan = 10, TenMan = "2x3 trung bình", Ngang = 2, Doc = 3, ThoiGian = 30, MaCapDo = 2 });
        lstManChoi.Add(new ManChoi() { MaMan = 11, TenMan = "2x4 trung bình", Ngang = 2, Doc = 4, ThoiGian = 35, MaCapDo = 2 });
        lstManChoi.Add(new ManChoi() { MaMan = 12, TenMan = "2x5 trung bình", Ngang = 2, Doc = 5, ThoiGian = 40, MaCapDo = 2 });
        lstManChoi.Add(new ManChoi() { MaMan = 13, TenMan = "3x2 trung bình", Ngang = 3, Doc = 2, ThoiGian = 30, MaCapDo = 2 });
        lstManChoi.Add(new ManChoi() { MaMan = 14, TenMan = "3x3 trung bình", Ngang = 3, Doc = 3, ThoiGian = 35, MaCapDo = 2 });
        lstManChoi.Add(new ManChoi() { MaMan = 15, TenMan = "3x4 trung bình", Ngang = 3, Doc = 4, ThoiGian = 40, MaCapDo = 2 });
        lstManChoi.Add(new ManChoi() { MaMan = 16, TenMan = "3x5 trung bình", Ngang = 3, Doc = 5, ThoiGian = 45, MaCapDo = 2 });
        #endregion
        // lấy danh sách hình từ resource
        sourceSprites = Resources.LoadAll<Sprite>("icon/Images");
  
    }
    // Start is called before the first frame update
    void Start()
    {
        ManHienTai = 0;
        this.ngang = lstManChoi[ManHienTai].Ngang;
        this.doc = lstManChoi[ManHienTai].Doc;
        StartLevel();

    }

    void StartLevel()
    {
        CreateButton();
        GetButton();
        AddButtonListener();
        AddImageForButton();
    }

    void AddImageForButton()
    {
        sprites = new List<Sprite>();
        for (int i = 0; i < ListBtn.Count; i++)
        {
            Sprite sprite = null;
            sprites.Add(sprite);
        }
        numMatch = 0;
        numWin = ListBtn.Count / 2;
        if (ListBtn.Count % 2 == 0)
        {
            for (int i = 0; i < numWin; i++)
            {
                int randImage = Random.Range(0, sourceSprites.Length - 1);
                int dem = 0;
                while (dem < 2)
                {
                    int randButton = Random.Range(0, sprites.Count);
                    if (sprites[randButton] == null)
                    {
                        sprites[randButton] = (sourceSprites[randImage]);
                        dem++;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < numWin; i++)
            {
                int randImage = Random.Range(0, sourceSprites.Length - 1);
                int dem = 0;
                while (dem < 2)
                {
                    int randButton = Random.Range(0, sprites.Count);
                    if (sprites[randButton] == null)
                    {
                        sprites[randButton] = (sourceSprites[randImage]);
                        dem++;
                    }
                }
            }
            numWin += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartNewLevel()
    {
        ManHienTai++;//tăng màn
        if (ManHienTai == lstManChoi.Count) //hết màn, quay lại từ đầu
        {
            ManHienTai = 0;
        }
        //Xóa mấy cái nút màn trước
        foreach (var item in lstBtnCreate)
        {
            Destroy(item);
        }
        //Set lại mấy thuộc tính
        this.ngang = lstManChoi[ManHienTai].Ngang;
        this.doc = lstManChoi[ManHienTai].Doc;
        ListBtn = new List<Button>();
        firstButtonClick = false;
        secondButtonClick = false;
        numMatch = 0;
        numWin = 0;
        Panel.DetachChildren();
        ListBtn = new List<Button>();
        sprites = new List<Sprite>();
        //Khởi động màn mới
        StartLevel();
    }

    void CreateButton()
    {
        //Tạo nút
        for (int i = 0; i < this.ngang * this.doc; i++)
        {
            btn = Instantiate(Button);
            //
            string tag = "Button" + lstManChoi[ManHienTai].MaMan;
            btn.tag = tag;
            btn.name = "" + i;
            btn.transform.SetParent(Panel, false);
            lstBtnCreate.Add(btn);
        }
    }

    void GetButton()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Button" + lstManChoi[ManHienTai].MaMan);

        for (int i = 0; i < objs.Length; i++)
        {
            ListBtn.Add(objs[i].GetComponent<Button>());
            ListBtn[i].image.sprite = background_image;
            ListBtn[i].GetComponentInChildren<Text>().text = "";
        }
        Panel.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        Panel.GetComponent<GridLayoutGroup>().constraintCount = this.ngang;
        Panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(100, 100);
    }

    void AddButtonListener()
    {
        foreach (var btn in ListBtn)
        {
            btn.onClick.AddListener(() => PickButton());
        }
    }

    void PickButton()
    {
        if (!firstButtonClick)
        {
            firstButtonClick = true;
            firstIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            if (sprites[firstIndex] == null)
            {
                ListBtn[firstIndex].interactable = false;
                ListBtn[firstIndex].image.color = new Color(0, 0, 0, 0);
                firstButtonClick = false;
                numMatch++;
                return;
            }
            firstButtonName = sprites[firstIndex].name;
            ListBtn[firstIndex].image.sprite = sprites[firstIndex];
            Debug.Log("list index: " + firstIndex + "1 name " + firstButtonName);
        }
        else
        {
            if (!secondButtonClick)
            {
                secondButtonClick = true;
                secondIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
                if (sprites[secondIndex] == null)
                {
                    ListBtn[secondIndex].interactable = false;
                    ListBtn[secondIndex].image.color = new Color(0, 0, 0, 0);
                    numMatch++;
                    secondButtonClick = false;
                    return;
                }
                if(secondIndex != firstIndex)
                {
                    secondButtonName = sprites[secondIndex].name;
                    ListBtn[secondIndex].image.sprite = sprites[secondIndex];
                    Debug.Log("list index: " + secondIndex + "2 name " + secondButtonName);
                    StartCoroutine(CheckMatch());
                }
                else
                {
                    secondButtonClick = false;
                }
               
            }
        }
    }

    IEnumerator CheckMatch()
    {
        if (firstIndex != secondIndex && firstButtonName.Equals(secondButtonName))
        {
            yield return new WaitForSeconds(1);
            ListBtn[firstIndex].interactable = false;
            ListBtn[secondIndex].interactable = false;
            ListBtn[firstIndex].image.color = new Color(0, 0, 0, 0);
            ListBtn[secondIndex].image.color = new Color(0, 0, 0, 0);
            firstButtonClick = false;
            secondButtonClick = false;
            numMatch++;
            if (numMatch == numWin)
            {
                Debug.Log("Win");
                StartNewLevel();
            }
        }
        else if (firstIndex != secondIndex)
        {
            Invoke("DownButton", 1);
        }
        else
        {

        }
    }
    void DownButton()
    {
        ListBtn[firstIndex].image.sprite = background_image;
        ListBtn[secondIndex].image.sprite = background_image;
        firstButtonClick = false;
        secondButtonClick = false;
    }


}
