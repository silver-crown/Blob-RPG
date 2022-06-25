# IMT3602 Professional Programming
author: Victor Sebastian Standal Clausen (vsclause)


This is the individual part of the Professional Programming course where I discuss some code and what I think about the term professionalism. 

## Code that I Consider Good

Some of the code I made for Behaviour Tree repeater node I consider to be decent; the code below is from the file BbbtRepeater.cs. This code is quite simple and that's perhaps the reason it turned out to be not too bad since it leaves fewer areas to make mistakes.

###  Example of code

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

namespace Bbbt
{
    // Creates the menu option in the Unity Engine
    [CreateAssetMenu(
        fileName = "Repeater",
        menuName = "bbBT/Behaviour/Decorator/Repeater",
        order = 0)]

    /// <summary>
    /// Repeats a child node for a set amount of times
    /// </summary>
    public class BbbtRepeater : BbbtDecoratorBehaviour
    {
        /// <summary>
        /// Number of times to repeat the child successfully
        /// </summary>
        [JsonProperty, SerializeField] private int Count;

        /// <summary>
        /// how many times the child has repeated
        /// </summary>
        private int _successCount;

        public override string SaveDataType { get; } = "BbbtRepeater";

        protected override void OnInitialize(GameObject gameObject)
        {
            Count = 0;
        }

        protected override void OnTerminate(GameObject gameObject, BbbtBehaviourStatus status)
        {
            
        }

        /// <summary>                                      
        /// Executes the repeaters child node until it has successfully executed a set number of times (Count variable).
        /// </summary>
        /// <param name="gameObject">Game object that owns the behaviour.</param>
        /// <returns>
        ///     BbbtBehaviourStatus.Running : if it's child is still running
        ///     BbbtBehaviourStatus.Failure : if it gets failure from its child a single time
        ///     BbbtBehaviourStatus.Success : this means the child has succsessfully updated 'Count' number of times.
        /// </returns>
        protected override BbbtBehaviourStatus UpdateBehaviour(GameObject gameObject)
        {
            while(true)
            {
                BbbtBehaviourStatus childStatus = Child.Tick(gameObject); 

                if (childStatus == BbbtBehaviourStatus.Running)
                {
                    return BbbtBehaviourStatus.Running;
                }

                if(childStatus == BbbtBehaviourStatus.Failure)
                {
                    return BbbtBehaviourStatus.Failure;
                }

                if(childStatus == BbbtBehaviourStatus.Success)
                {
                    Debug.Log("Updated Repeater Succsessfully");
                    _successCount++;
                }

                if(Count == _successCount)
                {
                    return BbbtBehaviourStatus.Success; 
                }
            }
        }
    }
}

```


## Discussion of "Good" Code

I think this code is rather good, mostly because it is a result of reading a lot of literature, especially the game AI pro series http://www.gameaipro.com. 
Adding this Decorator node, to the Behaviour Tree is not me reinventing anything but implementing it similar to how the game industry has done some
of it, even thought my code is simpler I still think it's an okay way of doing it. To my mind the comments written makes sense, and tries to help the
reader understand what is going on, the comments could perhaps have been a little bit better since it might require some knowledge of Behaviour Trees to get 
the most out of them.

I think the code is relatively simple to understand, and the variable names used makes sense and are not just single character names.
There is a while(true) loop in the code, which I know some people dislike, but in this case I think it makes clear what is going on in the code,
and I did not think of any other way to do it so currently I'm using this solution. I'm sure there are lots of things in this code example that could have been better, so I'm not claiming that this is perfect in any sense, there is almost always some way to improve especially as things get larger.

## Code I Consider Bad


```csharp

        /// <summary>
        /// Loops over all potential enemies for the actor and checks if they
        /// are within the actors Line Of Sight
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns>
        ///     BbbtBehaviourStatus.Failure; if no enemy is visible
        ///     BbbtBehaviourStatus.Success; if at least one enemy was spotted
        /// </returns>
        protected override BbbtBehaviourStatus UpdateBehaviour(GameObject gameObject)
        {
             _actor.VisibleEnemies.Clear();

            switch (_actor.MyFaction)
            {

                case Factions.Drone:

                    for (int i = 0; i < WorldInfo.Marines.Count; i++)
                    {
                        if (Vector3.Distance(_actor.transform.position, WorldInfo.Marines[i].transform.position) <= (float)_actor.GetValue("_sightRange").Number)
                        {
                            // Records last sighting of the player
                            _actor.LastSighting = WorldInfo.Marines[i].transform.position;

                            _actor.EnemyInSight = true;

                            // checks that the detected player is not already inn the list, this is to avoid
                            // the same enemy beeing added multiple times in the same list.
                            if (!_actor.VisibleEnemies.Contains(WorldInfo.Marines[i]))
                            {
                                _actor.VisibleEnemies.Add(WorldInfo.Marines[i]);
                            }
                        }
                    }

                    // If VisibleEnemies is 0 we have lost sight of the enemie(s)
                    if (_actor.VisibleEnemies.Count == 0)
                    {
                        _actor.EnemyInSight = false;
                        return BbbtBehaviourStatus.Failure;
                    }
                    else
                    {
                        return BbbtBehaviourStatus.Success;
                    }


                case Factions.Elders:

                    // If VisibleEnemies is 0 we have lost sight of the enemie(s)
                    if (_actor.VisibleEnemies.Count == 0)
                    {
                        _actor.EnemyInSight = false;
                        return BbbtBehaviourStatus.Failure;
                    }
                    else
                    {
                        return BbbtBehaviourStatus.Success;
                    }

                case Factions.Marine:


                    // If VisibleEnemies is 0 we have lost sight of the enemie(s)
                    if (_actor.VisibleEnemies.Count == 0)
                    {
                        _actor.EnemyInSight = false;
                        return BbbtBehaviourStatus.Failure;
                    }
                    else
                    {
                        return BbbtBehaviourStatus.Success;
                    }

                default:
                    return BbbtBehaviourStatus.Failure;
            }
        }
    }
}

```


### Discussion of Bad Code
From the file BbbtAllEnemiesInSight.cs

The switch statement uses a lot of code duplication so if there is a need to change the code in the future it has to be edited
in a lot of places, this makes errors occurring more likely since one might forget to update/change the code in one of the branches while updating
the code in other branches.

In retrospect I'm not too fond of the class name "BbbtAllEnemiesInSight" should probably have used "VisibleEnemies" or "AllvisibleEnemies" instead 
of "AllEnemiesInSight", there are also a few grammar errors in the comments that should be fixed.
This being said my main issue with it is still the switch statement taking so much space.

## Refactoring the Bad Code Example

```csharp

    /// <summary>
    /// Loops over all potential enemies for the actor and checks if they
    /// are within the actors Line Of Sight
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>
    ///     BbbtBehaviourStatus.Failure; if no enemy is visible
    ///     BbbtBehaviourStatus.Success; if at least one enemy was spotted
    /// </returns>
    protected override BbbtBehaviourStatus UpdateBehaviour(GameObject gameObject)
        { 
            _actor.VisibleEnemies.Clear();
            for (int i = 0; i < WorldInfo.Actors.Count; i++)
            {
                if(WorldInfo.Actors[i].MyFaction == _actor.MyFaction)
                {
                    continue;
                }
                if (Vector3.Distance(_actor.transform.position, WorldInfo.Actors[i].transform.position) <= (float)_actor.GetValue("_sightRange").Number)
                {
                    _actor.LastSighting = WorldInfo.Actors[i].transform.position;
                    _actor.EnemyInSight = true;
                     if (!_actor.VisibleEnemies.Contains(WorldInfo.Actors[i]))
                     {
                        _actor.VisibleEnemies.Add(WorldInfo.Actors[i]);
                     }
                } 
            }
            if (_actor.VisibleEnemies.Count == 0)
            {
                _actor.EnemyInSight = false;
                return BbbtBehaviourStatus.Failure;
            }
            else
            {
                return BbbtBehaviourStatus.Success;
            }
        }

```
This is the code in UpdateBehaviour after I refactored it for this discussion part, I think it looks a lot better
now that it takes up less space. Another way to refactor this could perhaps to use a method that took the
GameObject's Factions as an argument.

## Professionalism in Programming

To me Professionalism is mostly an attitude, and have some general traits that works across fields, but some of these traits are also specific
to engineering or programming.


#### Communication
Communication, make yourself available to the people you work with, perhaps have time slots when you are available this could for example be 10.00 to 15.00 Monday to Friday I will be in my office,
or  I check my emails 3'o clock every day.

#### Meetings
Meetings, it is important to show up to meetings and other planned events, if you can't make a meeting inform the people beforehand. 
This makes you reliable and avoid giving people negative surprises. If you don't show up people will not trust you.

#### Take initiative
Take initiative, don't just wait for other people to start tackling a problem. To me a lack of initiatve gives of a sense 
that one is not interessted in working on the task and to other people it might signal a lack of careing which of courese may or 
may not be the case.

#### Prioritize Work
Prioritize your work and your customers, people have things they value differently in their life but for me professionalism means your work is more
important than playing video games with your friends, if work and hobbies collide I personally think work takes the priority. If you choose your own entertainment
over potential customers, they will not feel valued, and might find a competitor who they feel prioritize them more.

#### Emotional Maturity
Don't display to many negative emotions while working with people, this can bring the group down and make people not want to engage with you.
Emotional maturity is something I don't think one will ever master, but is always something one can work towards. By this I am not saying you 
should let people walk over you or anything, but I'm sure we have all experienced having a negative state of mind interrupted by some important
conversations where we had to behave in such a way we presented our self positively, this video by Sam Harris Author and neuroscientistis my 
insperation for thinking more about the state of mind one is in https://www.youtube.com/watch?v=pTrrRoBZSpg.

#### Customer Relations
If one works with customers don't reduce the work to just about money received for static tasks, try and see it from their point of view. 
If they are launching a website for their small company understand that this is important to them and place yourself in their shoes, lets say they are making
decisions that you considered bad don't just take their money and run with it but communicate perhaps other solutions. This is the customers future, so it is important you care about the project.
I also tink one should keep a certain distance form customers and not get to personoal, since it can distract the process.

#### Being productive
No one likes pressure, but some level of results has to be produced by a person in any profession. In the programming profession it 
can perhaps be a bit easier to not be noticed when your skipping a day of work and browsing to much, than for people who work in other types
of profession, but of course this will eventually catch up to you.

I think a professional way of being productive is to be honest about what your capable of when it comes to work hrs, don't just try and work every day
all the time since it will most likely result in exhaustion. I think it is important to give yourself some time to work every day undistracted,
this means take one or two hours in the morning where you work on code, and then check emails etc after you have gotten this work done. This is to 
avoid distractions and getting some work inn.

One small extra thing I noticed is that a few programmers overestimate how fast they can get things done, I don't think promising
to deliver things on an unrealistic schedule is helpful for any one in the end. I am not sure if this is like dunn kruger effect, or 
something else but just something I noticed. However, they will probably make more accurate predictions as they gain experience.

### Programming 
This section contains somethings that I consider to professional that are more related to programming more specifically and 
focuses on engineering principles.


#### Use Design Patterns
Sometimes when solving a problem, it can be a good idea to research if someone else has already worked on this set of problems, 
to learn from them. Some problems are so often solved that patterns on how to solve them have been developed and used by a large
number of people. These patterns are tried and tested and if there exists a software pattern for the problem you are working on
it is a good idea to use it. However, don't over engineer the solution that are being worked on, if a pattern is not needed in the
project there is no reason to implement it.

#### Write good documentation
We write code not just for computers but also sometimes for other people to read. Having code that is easy
to read and understand is very important since it will increase productivity of the people you are working with.
Some good ideas can be to generate HTML documentation using special tags in your comments, or you can make a manual, my 
favourite is having online documentation like the company Oracle has for java https://docs.oracle.com/javase/7/docs/api/. Making a nice
site for your documentation will be time consuming and is perhaps a bit much for a small group of people, or for a small project
it might not be necessary. But one should always try and write some documentation for the complex tasks your code performs.


#### Keep it simple
Keep it simple, this is something I come to believe more inn as time goes on, making a simple solution does not mean it's easy. Solving a problem
in a weird way only you understand is sometimes faster than making a solution that takes little time for other developers to understand. 
Overengineered code is tiresome to read, and it makes people not want to engage with those parts of the software and maintaining it
becomes more difficult that it needs to be.

#### Testing and Writing Tests

Testing the application as it's being developed is critical to preventing errors, sometimes developers won't even run their code before 
committing a change, this is not a good way to go about developing software. To me testing should be done in several ways, testing the algorithms
your implementing making sure they work correctly, having integration testing as features are added to the whole system.

Unit Tests are a great way to automate testing and quickly discover if something has broken your code. I prefer to add colours in 
the terminal to these types of test when I use them but that's not really relevant the main point is that testing is something that can
and should be automated where suitable to detect when things are broken or stop working. 

Of course testing does not guarantee 100% that what you have done is correct but it gives you an indicator.

#### Security

Security is now something one should think about from the start of developing any serious project, and not a thing one
simply builds inn to after the project has been completed. The professional way to go about security would be to follow standards, read up on 
common mistakes on OWASP or something similar, do code audits and hire a computer security expert if the company can afford it.

Storing passwords in encrypted and salted version, use third party  authentication like google or Facebook for logins.
If one is to make an online payment system use a third-party solution for handling safe payment instead of developing your own.
Using third party entities that specialise in certain areas of software should perhaps been a point of it's own since I think it is very important.



