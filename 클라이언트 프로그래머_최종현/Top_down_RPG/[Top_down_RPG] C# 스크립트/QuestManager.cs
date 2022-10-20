using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("희귀한 동전?", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("퀘스트 완료!", new int[] { 0 }));
    }


    public int GetQuestTalkIndex(int npcId)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // Next Talk Target
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        ControlObject();

        // Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length) // 퀘스트 대화를 모두 다 했다면 퀘스트 번호 증가
            NextQuest();

        return questList[questId].questName; // 퀘스트 이름 출력
    }

    public string CheckQuest()
    {
        return questList[questId].questName; // 퀘스트 이름 출력
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    // 퀘스트 관리 함수
    public void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2) // 대화 2번이 완료되었으면
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                else if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                break;
        }
    }

}
