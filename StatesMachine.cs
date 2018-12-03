using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMachine : MonoBehaviour {

    public FollowMesh smallfollow = new FollowMesh();

    State currentState = null;

    //actions
    goDrink drink = new goDrink();
    followRed followR = new followRed();

    //conditions
    RedisDrinkingWaterCondition condition1 = new RedisDrinkingWaterCondition();
    RedisnotDrinkingWaterCondition condition2 = new RedisnotDrinkingWaterCondition();

    void Start()
    {   
        //transitions
        Transition seeRedDragondrinking = new Transition();
        Transition seeRedDragonleavingwaterplace = new Transition();

        //states
        State initialState = new State();
        initialState.action = -1;
        initialState.transitions.Add(seeRedDragondrinking);

        State drinking = new State();
        drinking.action = 1;
        drinking.transitions.Add(seeRedDragonleavingwaterplace);

        State followingRed = new State();
        followingRed.action = 2;
        followingRed.transitions.Add(seeRedDragonleavingwaterplace);


        seeRedDragondrinking.targetState = drinking;
        seeRedDragondrinking.condition = 1;

        seeRedDragonleavingwaterplace.targetState = followingRed;
        seeRedDragonleavingwaterplace.condition = 2;

        currentState = initialState;
    }

    public class goDrink{
        public object run(FollowMesh follow)
        {   
            follow.end = 14;
            follow.Go(follow.SmallDragon);
            return null;
        }
    }

    public class followRed
    {
        public object run(FollowMesh follow)
        {
            follow.end = 131; 
            follow.Go(follow.SmallDragon);
            return null;
        }
    }

    public class State {
        public int action;
        
        public int getAction() {
            return action;
        }

        public List<Transition> transitions = new List<Transition>();
        public List<Transition> getTransitions() {
            return transitions;
        }

    }

    public class Transition {
        public State targetState;
        public State getTargetState() {
            return targetState;
        }

        public int condition;
     
    }

    public class RedisDrinkingWaterCondition{
        
        public bool test(FollowMesh follow) {

            if ((follow.getDragonNode(follow.RedDragon) == 14 || follow.getDragonNode(follow.RedDragon) == 15 || follow.getDragonNode(follow.RedDragon) == 16 || follow.getDragonNode(follow.RedDragon) == 17) && follow.canbeupdated == true)
            {
                return true;
            }
            return false;
        }
    }

    public class RedisnotDrinkingWaterCondition
    {

        public bool test(FollowMesh follow)
        {

            if ((follow.getDragonNode(follow.RedDragon) != 14 && follow.getDragonNode(follow.RedDragon) != 15 && follow.getDragonNode(follow.RedDragon) != 16 && follow.getDragonNode(follow.RedDragon) != 17) && follow.canbeupdated == true)
            {
                return true;
            }
            return false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        foreach (Transition transition in currentState.getTransitions())
        {

            if (transition.condition == 1 && condition1.test(smallfollow) == true)
            {
                currentState = transition.targetState;
                if (currentState.action == 1)
                {
                    drink.run(smallfollow);
                }
            }
            else if (transition.condition == 2 && condition2.test(smallfollow) == true && (smallfollow.getDragonNode(smallfollow.SmallDragon)!=131))
            {
                currentState = transition.targetState;
                if (currentState.action == 2)
                {
                    followR.run(smallfollow);
                }
            }
        }
     
    }

}
