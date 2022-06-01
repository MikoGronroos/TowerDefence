using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GetNistTime
{
    public static DateTime GetNISTDate()
    {
        int timeout = 300;
        var result = DateTime.MinValue;
        // Initialize the list of NIST time servers
        // http://tf.nist.gov/tf-cgi/servers.cgi
        string[] serversArray = new string[] {
            "time-a-g.nist.gov",
            "time-b-g.nist.gov",
            "time-c-g.nist.gov",
            "time-d-g.nist.gov",
            "time-e-g.nist.gov",
            "time-a-wwv.nist.gov",
            "time-b-wwv.nist.gov",
            "time.nist.gov"
        };
        List<string> servers = new List<string>(serversArray);
        // Try 5 servers in random order to spread the load
        while (servers.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, servers.Count);
            string server = servers[index];
            servers.RemoveAt(index);
            try
            {
                // Connect to the server (at port 13) and get the response
                string serverResponse = string.Empty;
                System.Net.Sockets.TcpClient tcpClient;
                tcpClient = new System.Net.Sockets.TcpClient();

                var resultOfConnection = tcpClient.BeginConnect(server, 13, null, null);
                int millisecondsEnter = DateTime.UtcNow.Millisecond;
                var success = resultOfConnection.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout));
                int millisecondsExit = DateTime.UtcNow.Millisecond;

                tcpClient.Client.ReceiveTimeout = timeout;

                if (success)
                {
                    tcpClient.GetStream().ReadTimeout = timeout;
                    serverResponse = (new StreamReader(tcpClient.GetStream())).ReadToEnd();
                    tcpClient.Close();
                }
                else
                {
                    tcpClient.Close();
                    throw new Exception("Failed to connect.");
                }

                // If a response was received
                if (!string.IsNullOrEmpty(serverResponse))
                {
                    // Split the response string ("55596 11-02-14 13:54:11 00 0 0 478.1 UTC(NIST) *")
                    string[] tokens = serverResponse.Split(' ');

                    // Check the number of tokens
                    if (tokens.Length >= 6)
                    {
                        // Check the health status
                        string health = tokens[5];
                        if (health == "0")
                        {
                            // Get date and time parts from the server response
                            string[] dateParts = tokens[1].Split('-');
                            string[] timeParts = tokens[2].Split(':');

                            // Create a DateTime instance
                            DateTime utcDateTime = new DateTime(
                                Convert.ToInt32(dateParts[0]) + 2000,
                                Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]),
                                Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]),
                                Convert.ToInt32(timeParts[2]));

                            // Convert received (UTC) DateTime value to the local timezone
                            result = utcDateTime;

                            return result;
                            // Response successfully received; exit the loop

                        }
                    }

                }

            }
            catch
            {
                // Ignore exception and try the next server
            }
        }
        return result;
    }
}
