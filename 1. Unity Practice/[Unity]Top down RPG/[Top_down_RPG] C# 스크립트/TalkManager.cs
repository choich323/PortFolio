using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; // 두 개의 타입을 적어야 함(키, 밸류)
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        // 대화
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳은 처음이지?:2" });
        talkData.Add(2000, new string[] { "이 뒤로는 갈 필요없어.:0", "가봤자 아무것도 없거든.:1", "못 믿겠으면 직접 확인해보던가.:2" });

        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "Luna가 준비한 책상이다." });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "어서 와.:0", "이 곳은 처음이지?:2", "아직 조촐한 단계지만, 점점 발전할거야.:1", "우선 저기 오른쪽의 루도에게 가봐.:0" });
        talkData.Add(11 + 2000, new string[] { "또 루나가 나한테 오라고 했나?.:0", "아쉽지만 이 다음은 정해지지 않아서 말이야.:1", "힘들게 왔는데 미안하게 됐군.:0", 
                                                "아, 그렇지. 괜찮다면 동전이라도 받아가겠어?:1", "동전이라고 무시하지 말라고.:2", "금으로 된 동전이라 비싼 값에 팔리니까.:0", 
                                                "그런데 내가 이 근처에 떨어트렸단 말이야.:1", "혹시 찾게 된다면 가져도 좋아.:0" });

        talkData.Add(20 + 2000, new string[] { "분명 근처에서 동전을 찾을 수 있을거야.:1" });
        talkData.Add(20 + 1000, new string[] { "루도가 동전을 찾아보라고 했다고?:0", "별일이네. 루도가 그런 걸 다 시키고 말이야.:3" });
        talkData.Add(20 + 5000, new string[] { "루도의 동전을 찾았다." });

        talkData.Add(21 + 2000, new string[] { "오, 정말 찾은건가? 제법인데?:2", "동전은 약속대로 가져도 좋아. 유용하게 쓰라고.:1" });

        // 초상화
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id)) // dictionary에 key가 있는지 검사
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex); // Get First Talk
            else
                return GetTalk(id - id % 10, talkIndex); // Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
