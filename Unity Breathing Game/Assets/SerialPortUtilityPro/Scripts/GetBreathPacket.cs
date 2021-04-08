using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GetBreathPacket : MonoBehaviour
{
    // To get data from script that handles serial input
    private SerialPortUtility.SerialPortUtilityPro serialScript;

    // Final integer breath values
    public int abdBreathValue;
    public int chBreathValue;

    // Data of relevance
    public int abdHighByte;
    public int abdLowByte;
    public int chestHighByte;
    public int chestLowByte;    

    // The entire raw incoming packet as integer values
    public byte[] packet;

    public float timer;
    private string temp;


    private Callibration callibration;
    ButtonsManager buttonsManagerScript;

    private void Awake()
    {
        serialScript = GameObject.FindObjectOfType<SerialPortUtility.SerialPortUtilityPro>();
        callibration = GameObject.FindObjectOfType<Callibration>();
        buttonsManagerScript = GameObject.FindObjectOfType<ButtonsManager>();
    }


    // All input byte data cleaning, escape sequence addition, etc.
    public void GetThePacket(byte[] dataPacket)
    {
        packet = dataPacket;
        timer = Mathf.Round(Time.time * 1000f) / 1000f;
        //Debug.Log(timer);

        // According to relevant bytes as ordered in the incoming packet
        // If no escape sequence involved, byte number 2,3,4,5 are required to be extracted in my case, could be different per device/byte sequence
        if (!(packet[2] == 125 | packet[3] == 125 | packet[4] == 125 | packet[5] == 125))
        {
            abdHighByte = packet[2];
            abdLowByte = packet[3];
            chestHighByte = packet[4];
            chestLowByte = packet[5];
        }

        // Next 4 if conditions are for escape sequence handling. Comments in the first if loop are relevant to all others.
        if (packet[2] == 125)
        {

            temp = packet[3].ToString("X"); // Hex value of the byte from which the required data needs to be retrieved
            abdHighByte = packet[3] ^ 32;   // To get back the value that caused the escape sequence to occur. Byte 2 is replaced by an escape value, which shifts the corresponding values one place ahead.
           /* Debug.Log("TEMP: " + temp);
            Debug.Log("ABD HIGH BYTE:" + abdHighByte);*/
            abdLowByte = packet[4];         // Abd low byte then becomes packet[4] and so on..
            chestHighByte = packet[5];
            chestLowByte = packet[6];
        }

        if (packet[3] == 125)
        {
            abdHighByte = packet[2];
            temp = packet[4].ToString("X");
            abdLowByte = packet[4] ^ 32;
            /*Debug.Log("TEMP: " + temp);
            Debug.Log("ABD LOW BYTE:" + abdLowByte);*/
            chestHighByte = packet[5];
            chestLowByte = packet[6];
        }

        if (packet[4] == 125)
        {
            abdHighByte = packet[2];
            abdLowByte = packet[3];
            chestHighByte = packet[5] ^ 32;
            temp = packet[5].ToString("X");
            /*Debug.Log("TEMP: " + temp);
            Debug.Log("CHEST HIGH BYTE:" + chestHighByte);*/
            chestLowByte = packet[6];
        }

        if (packet[5] == 125)
        {
            abdHighByte = packet[2];
            abdLowByte = packet[3];
            chestHighByte = packet[4];
            chestLowByte = packet[6] ^ 32;
            temp = packet[6].ToString("X");
            /*Debug.Log("TEMP: " + temp);
            Debug.Log("CHEST LOW BYTE:" + chestLowByte);*/
        }

        // Once escape sequence handling is done, set the integer values by combining the bytes:
        if(abdHighByte > 0 & chestHighByte > 0)
        {
            abdBreathValue = abdLowByte + (abdHighByte * 256);
            chBreathValue = chestLowByte + (chestHighByte * 256);
        }

        // Send relevant data to csv file handler
        CSVManager.AppendToReport(GetReportLine());

    }

    // Sends data to csv tool created
    string[] GetReportLine()
    {
        string[] returnable = new string[3];    // Adjust according to how much data you need to send to csv files
        returnable[0] = timer.ToString();
        returnable[1] = abdBreathValue.ToString();
        returnable[2] = chBreathValue.ToString();

        return returnable;
    }

}