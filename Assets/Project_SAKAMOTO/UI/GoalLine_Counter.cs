using UnityEngine;

public class GoalLine_Counter : MonoBehaviour
{
   [SerializeField] private bool isPassFront;
   [SerializeField] private bool isPassBack;

    [SerializeField] private int startCount;

    RaceLap_Manager racelap_manager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPassFront = false;
        isPassBack = false;
        startCount = 0;
        racelap_manager =FindAnyObjectByType<RaceLap_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GoalFront"))
        {
            if(!isPassBack)
            {
                isPassFront = true;
                return;
            }
            else if(isPassBack)
            {

                    --racelap_manager.LapCount_1p;
                    isPassFront = false;
                    isPassBack = false;
                    return;
                
            }
        }

        if (other.CompareTag("GoalBack"))
        {
            if (!isPassFront)
            {
                isPassBack = true;
                return;
            }
            else if (isPassFront)
            {
                if (startCount == 0)
                {
                    ++startCount;
                    return;
                }
                else if (startCount > 0)
                {
                    ++racelap_manager.LapCount_1p;
                    isPassFront = false;
                    isPassBack = false;
                    return;
                }
            }
        }
    }

}
