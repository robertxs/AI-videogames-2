using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMachine : MonoBehaviour {

    public FollowMesh smallfollow = new FollowMesh();

    State currentState = null;

   

    

    void Start()
    {
        //actions
        goDrink drink = new goDrink();
        goFire fire = new goFire();
        jumpAndPlayBall play = new jumpAndPlayBall();
        goEat eat = new goEat();

        //conditions
        RedisDrinkingWaterCondition condition1 = new RedisDrinkingWaterCondition();
        RedisnotDrinkingWaterCondition condition2 = new RedisnotDrinkingWaterCondition();
        RedisplayingBallCondition condition3 = new RedisplayingBallCondition();
        Rediseating condition4 = new Rediseating();

        //transitions
        Transition seeRedDragondrinking = new Transition();
        Transition seeRedDragonleavingwaterplace = new Transition();
        Transition seeRedPlayingBall = new Transition();
        Transition seeRedEating = new Transition();

        //states
        State initialState = new State();
        initialState.action = null;
        initialState.transitions.Add(seeRedDragondrinking);

        State drinking = new State();
        drinking.action = drink;
        drinking.transitions.Add(seeRedDragonleavingwaterplace);

        State stayingAtFire = new State();
        stayingAtFire.action = fire;
        stayingAtFire.transitions.Add(seeRedPlayingBall);
        stayingAtFire.transitions.Add(seeRedEating);

        State playingBall = new State();
        playingBall.action = play;
        playingBall.transitions.Add(seeRedPlayingBall);
        playingBall.transitions.Add(seeRedEating);

        State eating = new State();
        eating.action = eat;
        eating.transitions.Add(seeRedPlayingBall);


        seeRedDragondrinking.targetState = drinking;
        seeRedDragondrinking.condition = condition1;

        seeRedDragonleavingwaterplace.targetState = stayingAtFire;
        seeRedDragonleavingwaterplace.condition = condition2;

        seeRedPlayingBall.targetState = playingBall;
        seeRedPlayingBall.condition = condition3;

        seeRedEating.targetState = eating;
        seeRedEating.condition = condition4;

        currentState = initialState;
    }

    public abstract class Action
    {
        public abstract void run(FollowMesh follow);
       
    }

    public class goDrink : Action{
        public override void run(FollowMesh follow)
        {   
            follow.end = 14;
            follow.Go(follow.SmallDragon);
        }
    }

    public class goFire : Action
    {
        public override void run(FollowMesh follow)
        {
            follow.end = 131; 
            follow.Go(follow.SmallDragon);
        }
    }

    public class jumpAndPlayBall : Action
    {
        public override void run(FollowMesh follow)
        {
            follow.SmallDragon.transform.rotation = Quaternion.Slerp(follow.SmallDragon.transform.rotation, Quaternion.LookRotation(follow.Ball.transform.position - follow.SmallDragon.transform.position), 0.04F);
            follow.SmallDragon.GetComponent<Jump>().executejump = true;       
        }
    }

    public class goEat : Action
    {
        public override void run(FollowMesh follow)
        {
            follow.end = 177;
            follow.Go(follow.SmallDragon);
        }
    }

    public class State {
        public Action action;
        
        public Action getAction() {
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

        public Condition condition;
     
    }

    public abstract class Condition
    {
        public abstract bool test(FollowMesh follow);
    }

    public class RedisDrinkingWaterCondition : Condition
        {
        
        public override bool test(FollowMesh follow) {

            if ((follow.getDragonNode(follow.RedDragon) == 14 || follow.getDragonNode(follow.RedDragon) == 15 || follow.getDragonNode(follow.RedDragon) == 16 || follow.getDragonNode(follow.RedDragon) == 17) && follow.canbeupdated == true)
            {
                return true;
            }
            return false;
        }
    }

    public class RedisnotDrinkingWaterCondition : Condition
        {

        public override bool test(FollowMesh follow)
        {

            if ((follow.getDragonNode(follow.RedDragon) != 14 && follow.getDragonNode(follow.RedDragon) != 15 && follow.getDragonNode(follow.RedDragon) != 16 && follow.getDragonNode(follow.RedDragon) != 17) && follow.canbeupdated == true)
            {
                return true;
            }
            return false;
        }
    }

    public class RedisplayingBallCondition : Condition
    {

        public override bool test(FollowMesh follow)
        {

            if ((follow.getDragonNode(follow.RedDragon) == follow.getDragonNode(follow.Ball) || follow.getDragonNode(follow.RedDragon) == follow.getDragonNode(follow.Ball)+1 || follow.getDragonNode(follow.RedDragon) == follow.getDragonNode(follow.Ball)-1) && follow.canbeupdated == true)
            {
                return true;
            }
            return false;
        }
    }

    public class Rediseating : Condition
    {

        public override bool test(FollowMesh follow)
        {

            if ((follow.getDragonNode(follow.RedDragon) == 177) && follow.canbeupdated == true)
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

            if (transition.condition.test(smallfollow) == true)
            {
                currentState = transition.targetState;
                currentState.action.run(smallfollow);
            }
            
        }
     
    }

}
