using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

struct ChatDateTime
{
    public int m_year;
    public int m_month;
    public int m_day;
    public int m_hour;
    public int m_minute;
    public int m_second;
    public int m_milisecond;

    public ChatDateTime(int y, int m, int d, int h, int min, int s, int mili)
    {
        this.m_year = y;
        this.m_month = m;
        this.m_day = d;
        this.m_hour = h;
        this.m_minute = min;
        this.m_second = s;
        this.m_milisecond = mili;
    }

}
public struct ChatContent
{
    private string m_playerName;
    private string m_message;
    private int m_contentID;
    private static int CONTENTPREVID;
    private ChatDateTime m_chatDateTime;
    

    public ChatContent(string playerName, string message)
    {
        CONTENTPREVID = 0;
        this.m_playerName = playerName;
        this.m_message = message;
        m_contentID = CONTENTPREVID;
        CONTENTPREVID++;
        m_chatDateTime = new ChatDateTime(
            System.DateTime.Today.Year,
            System.DateTime.Today.Month,
            System.DateTime.Today.Day,
            System.DateTime.Today.Hour,
            System.DateTime.Today.Minute,
            System.DateTime.Today.Second,
            System.DateTime.Today.Millisecond
            );
    }
    public string GetContent()
    {
        return this.m_message;
    }

    public string GetPlayerName()
    {
        return this.m_playerName;
    }

}

public class MultiplayerChat : MonoBehaviour, IChatClientListener
{
    public  Text m_textContent;
    public InputField m_chatInput;
    public GameObject m_chatPanel;
    private string m_tmpText = "asdasd";


    public List<ChatContent> m_chatContents;

    private ChatClient m_chatClient;

    public string m_worldChat;
    [SerializeField]
    private string m_userID;

    private void Start()
    {
        Application.runInBackground = true;

        GetConnected();
        if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat))
        {
            Debug.LogError("No Chat AppID provided");
            return;
        }
        m_worldChat = "World";

        m_chatContents = new List<ChatContent>();
    }



    void Update()
    {
        if (m_chatClient != null)
            m_chatClient.Service();


        if (Input.GetKeyDown(KeyCode.T))
            m_chatPanel.SetActive(!m_chatPanel.activeSelf);

        if (m_chatPanel.activeSelf)
            if (m_chatInput.isFocused)
                if (Input.GetKeyDown(KeyCode.E) && m_chatInput.text != null) 
                {
                    ChatContent _chat = new ChatContent(PhotonNetwork.NickName, m_chatInput.text);
                    SendMessage(_chat.GetContent());
                    m_chatContents.Add(_chat);
                    
                }
        if (Input.GetKeyDown(KeyCode.M))
            SaveEndOfMatchChat();

        
    }

    void SaveEndOfMatchChat()
    {
        BinaryFormatter _formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves/chats/ingame"))
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/chats/ingame");

        Debug.Log(Application.dataPath);

        string _path = Application.persistentDataPath + "/saves/chats/ingame"+ "/matchLobbyChat.chat";
        Debug.Log(Application.persistentDataPath);
        FileStream _stream = File.Create(_path);
        
        _formatter.Serialize(_stream, "Chat Messages: \n" + m_chatContents.ToString());
        _stream.Close();
    }

    public void GetConnected()
    {
        m_chatClient = new ChatClient(this);
        Debug.Log("Player Name is: " + PhotonNetwork.NickName);
        m_chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues("Person: "));
    }

    public void SendMessage(string message)
    {
        m_chatClient.PublishMessage(m_worldChat, message);
    }



    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnDisconnected()
    {
        
    }

    public void OnConnected()
    {
        Debug.Log("Chat is Connected!");
        m_chatClient.Subscribe(new string[] { m_worldChat });
        m_chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            //23:23
            m_textContent.text += senders[i] + ": " + messages[i] + " \n";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (var channel in channels)
        {
            this.m_chatClient.PublishMessage(channel, "Joined");
        }
        
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }
}
