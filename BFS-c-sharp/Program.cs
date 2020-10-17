using BFS_c_sharp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();
            Dictionary<UserNode, HashSet<UserNode>> dictonary = UploadDictionary(users);
            int steps = MinimumDistanceBetween2People(dictonary, users[3], users[25]);
            //GiveFriendsOfFriends(dictonary, users[0], 2);
            ShortestPathBetweenTwo(dictonary, users[3], users[22]);
            
            // foreach (var user in users)
            // {
            //     Console.WriteLine(user);
            // }
            //
            // Console.WriteLine("Done");
            // Console.ReadKey();
        }

        public static Dictionary<UserNode, HashSet<UserNode>> UploadDictionary(List<UserNode> users)
        {
           return users.ToDictionary(f => f, f => f.Friends);
        }


        public static void GiveFriendsOfFriends(Dictionary<UserNode, HashSet<UserNode>> Adjacent,UserNode user, int distance)
        {
            Console.WriteLine("Given user: "+ user);
            Console.WriteLine("Given Distance: " + distance);
            int level = 1;
            var alreadyVisited = new HashSet<UserNode>();
            var queue = new Queue<Tuple<UserNode,int>>();
            queue.Enqueue(Tuple.Create(user, 0));
            alreadyVisited.Add(user);
            foreach (UserNode friend in user.Friends)
            {
                queue.Enqueue(Tuple.Create(friend, level));
                alreadyVisited.Add(friend);
            }

            while (level < distance)
            {
                HashSet<UserNode> hashSet = new HashSet<UserNode>(queue.SelectMany(x => x.Item1.Friends.Where(c=>!alreadyVisited.Contains(c))));
                foreach (UserNode userNode in hashSet)
                {
                    queue.Enqueue(Tuple.Create(userNode, level + 1));
                    alreadyVisited.Add(userNode);
                }
                level++;
            }

            Console.WriteLine($"User you have choosen is {user} and the depth is {distance}, please see the friends of friends");
            foreach (UserNode userNode in alreadyVisited)
            {
                Console.WriteLine(userNode);
            }
        }

        public static void ShortestPathBetweenTwo(Dictionary<UserNode, HashSet<UserNode>> Adjacent, UserNode startNode, UserNode endNode)
        {
            var previous = new Dictionary<UserNode, UserNode>();
            var queue = new Queue<UserNode>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var neighbour in Adjacent[current])
                {
                    if (neighbour == endNode)
                    {
                        previous[neighbour] = current;
                        var path = new List<UserNode>();
                        int count = 0;
                        var currentNode = endNode;
                        while (!currentNode.Equals(startNode))
                        {
                            count++;
                            path.Add(currentNode);
                            currentNode = previous[currentNode];
                        }
                        path.Add(startNode);
                        path.Reverse();
                        Console.WriteLine(string.Join("-", path));
                        return;
                    }
                    
                    if (previous.ContainsKey(neighbour))
                    {
                        continue;
                    }

                    previous[neighbour] = current;
                    queue.Enqueue(neighbour);
                }
            }

            

        }
        

        public static int MinimumDistanceBetween2People(Dictionary<UserNode, HashSet<UserNode>> Adjacent, UserNode startNode, UserNode endNode)
        {
            Console.WriteLine("starter: " + startNode);
            Console.WriteLine("ender: " + endNode);
            var previous = new HashSet<UserNode>();
            var visited = new HashSet<UserNode>();
            visited.Add(startNode);
            var q = new Queue<UserNode>();
            q.Enqueue(startNode);
            int count = 1;
            
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                Console.WriteLine(current);
                if (Adjacent.ContainsKey(current))
                {
                    foreach (var neighbour in Adjacent[current].Where(a => !visited.Contains(a)))
                    {
                        if (neighbour == endNode)
                        {
                            Console.WriteLine("Found it");
                            Console.WriteLine("Steps took: " + count);
                            //previous.Add(neighbour);
                            //Console.WriteLine(string.Join("-", previous));
                            return count;
                        }
                        
                        visited.Add(neighbour);
                        q.Enqueue(neighbour);
                    }
                }

                //previous.Add(current);
                count++;
            }

            Console.WriteLine("whats going on Gyongyos");
            
            return 0;
        }

        public static int shortestPath(UserNode starter, UserNode ender,HashSet<UserNode> hashSet)
        {
            var path = new List<UserNode>();
            var current = starter;
            while (!current.Equals(ender))
            {
                path.Add(current);
              
            }

            return 0;
        }
    }
}
