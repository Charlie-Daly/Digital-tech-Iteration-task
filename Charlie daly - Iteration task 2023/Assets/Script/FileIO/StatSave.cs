using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StatSave : MonoBehaviour
{
    public int[] stats = new int[10];

    string currentDirectory;

    public string statFileName = "Stats.txt";

    // Start is called before the first frame update
    void Start()
    {
        //writing where the directory is
        currentDirectory = Application.dataPath;
        Debug.Log("our current directory is: " + currentDirectory);

        //load the scores by default
        LoadStatsFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStatsFromFile()
    {
        //Checking if the file exisits
        bool fileExists = File.Exists(currentDirectory + "\\" + statFileName);
        if (fileExists == true)
        {
            Debug.Log("Found stat file " + statFileName);
        }
        else
        {
            Debug.Log("The file " + statFileName +
                " does not exist. no stats will be loaded.", this);
            return;
        }

        //make an array of default values
        stats = new int[stats.Length];

        //read the file in
        StreamReader fileReader = new StreamReader(currentDirectory + "\\" + statFileName);

        //counter to make sure it doesn't go past the stats
        int statCount = 0;

        //a while loop that runs for as long as there is unread data
        //and statcount is not hit
        while (fileReader.Peek() != 0 && statCount < stats.Length)
        {
            // read line into variable
            string fileLine = fileReader.ReadLine();

            //parse that variable into an int
            //vatiable to put it in
            int readStats = -1;

            //parsing it
            bool didParse = int.TryParse(fileLine, out readStats);
            if (didParse)
            {
                //if it successfully read a number, it goes in a array.
                stats[statCount] = readStats;
            }
            else
            {
                //if its unsuccessfull, then it prints an error and uses a default value
                Debug.Log("Invaild line in stats file at " + statCount +
                            ", using defaule value", this);
                stats[statCount] = 0;
            }
            //increase counter
            statCount++;
        }

        //close the stream
        fileReader.Close();
        Debug.Log("Stats read from " + statFileName);
    }

    public void SaveStatsToFile()
    {
        // create a StreamWriter
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + statFileName);

        //write the lines to the file
        for (int i = 0; i < stats.Length; i++)
        {
            fileWriter.WriteLine(stats[i]);
        }

        //close the stream
        fileWriter.Close();

        //Log message
        Debug.Log("stats written to " + statFileName);
    }

    public void AddStat(int newStats)
    {
        //Check which stats are frist
        int desiredIndex = -1;
        for (int i = 0; i < stats.Length; i++)
        {
            //instrad of checking tha vaule of desiredIndex
            //we could also use 'break' to stop the loop
            if (stats[i] > newStats || stats[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }

        //if no desired index was found then the score
        //isn't high enough to get on the table, so we just abort
        if (desiredIndex < 0)
        {
            Debug.Log("Score of " + newStats +
                        " not high enough for the stats list.", this);
            return;
        }

        //then we move all of the scores after that index
        //back by one position. we'll do this by looping from
        //the back of the array to  our desired index.
        for (int i = stats.Length - 1; i > desiredIndex; i--)
        {
            stats[i] = stats[i - 1];
        }

        //insert our new score in its place
        stats[desiredIndex] = newStats;
        Debug.Log("Score of " + newStats +
                    " entered into stats at position " + desiredIndex, this);
    }
}
