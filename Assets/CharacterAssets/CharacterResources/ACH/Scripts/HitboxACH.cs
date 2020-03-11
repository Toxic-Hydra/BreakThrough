﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxACH : MonoBehaviour
{
    public BoxCollider2D hit1;
    public BoxCollider2D hit2;
    public BoxCollider2D hit3;
    public BoxCollider2D hit4;
    public BoxCollider2D hit5;
    public BoxCollider2D hit6;
    public BoxCollider2D hit7;

    public HitDetector HitDetect;
    public AttackHandlerACH AttackHandler;

    int hitStunLv1 = 10;
    int hitStunLv2 = 14;
    int hitStunLv3 = 17;
    int hitStunLv4 = 19;

    int hitStopLv1 = 8;
    int hitStopLv2 = 9;
    int hitStopLv3 = 10;
    int hitStopLv4 = 11;

    public int sinCharge; // variable used to charge/enhance Genesis Assault special attack, similar to sin charge from DHA


    void Start()
    {
        AwakenBox();
    }

    void Update()
    {
        if (HitDetect.hit)
        {
            ClearHitBox();
            HitDetect.hit = false;
        }

        if (HitDetect.hitStun > 0)
        {
            ClearHitBox();
            sinCharge = 0;
            HitDetect.anim.SetInteger("SinCharge", sinCharge);
        }
    }

    public void ClearHitBox()
    {
        hit1.enabled = false;
        hit2.enabled = false;
        hit3.enabled = false;
        hit4.enabled = false;
        hit5.enabled = false;
        hit6.enabled = false;
        hit7.enabled = false;
        HitDetect.potentialHitStun = 0;
        HitDetect.potentialHitStop = 0;
        HitDetect.damage = 0;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 0;
        HitDetect.potentialKnockBack = Vector2.zero;
        HitDetect.potentialAirKnockBack = Vector2.zero;
        HitDetect.forcedProration = 0;
        HitDetect.initialProration = 1;
        HitDetect.attackLevel = 0;
        HitDetect.grab = false;
        HitDetect.blitz = false;
        HitDetect.piercing = false;
        HitDetect.launch = false;
        HitDetect.crumple = false;
        HitDetect.sweep = false;
        HitDetect.forceCrouch = false;
        HitDetect.allowWallStick = false;
        HitDetect.allowGroundBounce = false;
        HitDetect.allowWallBounce = false;
        HitDetect.shatter = false;
        HitDetect.usingSuper = false;
        HitDetect.usingSpecial = false;
        HitDetect.slash = false;
        HitDetect.vertSlash = false;
        HitDetect.horiSlash = false;
    }

    void AwakenBox()
    {
        ClearHitBox();
        hit1.enabled = true;
        hit1.size = new Vector2(10f, 10f);
    }

    public void BlitzCancel()
    {
        if (HitDetect.OpponentDetector.hitStun > 0)
        {
            ClearHitBox();
            hit1.enabled = true;
            hit1.offset = new Vector2(.0f, .0f);
            hit1.size = new Vector2(4f, 4f);

            HitDetect.attackLevel = 0;
            HitDetect.guard = "Unblockable";

            HitDetect.blitz = true;
        }
    }

    //push damage values, knockback, and proration to hitdetector from hitbox events
    void StandingLHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();
        hit1.enabled = true;
        hit1.offset = new Vector2(.48f, .15f);
        hit1.size = new Vector2(.55f, .15f);
        HitDetect.damage = 25;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 50;
        HitDetect.potentialHitStun = hitStunLv1;
        HitDetect.potentialHitStop = hitStopLv1;
        HitDetect.potentialKnockBack = new Vector2(1.2f, 0);
        HitDetect.initialProration = .7f;
        HitDetect.attackLevel = 0;
        HitDetect.guard = "Mid";

        HitDetect.allowLight = true;
        HitDetect.allowMedium = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
    }

    void CrouchingLHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();
        hit1.enabled = true;
        hit1.offset = new Vector2(.5f, -.23f);
        hit1.size = new Vector2(.55f, .15f);
        HitDetect.damage = 20;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 50;
        HitDetect.potentialHitStun = hitStunLv1;
        HitDetect.potentialHitStop = hitStopLv1;
        HitDetect.potentialKnockBack = new Vector2(1f, 0);
        HitDetect.initialProration = .65f;
        HitDetect.attackLevel = 0;
        HitDetect.guard = "Mid";

        HitDetect.allowLight = true;
        HitDetect.allowMedium = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
    }

    void JumpLHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit1.offset = new Vector2(.55f, .3f);
        hit1.size = new Vector2(.57f, .17f);
        HitDetect.damage = 24;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 50;
        HitDetect.potentialHitStun = hitStunLv2;
        HitDetect.potentialHitStop = hitStopLv1;
        HitDetect.potentialKnockBack = new Vector2(1f, 0);
        HitDetect.potentialAirKnockBack = new Vector2(.7f, 2f);
        HitDetect.initialProration = .7f;
        HitDetect.attackLevel = 0;
        HitDetect.guard = "Overhead";

        HitDetect.allowLight = true;
        HitDetect.allowMedium = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
        HitDetect.jumpCancellable = true;
    }


    void StandingMHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit1.offset = new Vector2(.61f, -.41f);
        hit1.size = new Vector2(.81f, .271f);
        HitDetect.damage = 36;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv1;
        HitDetect.potentialHitStop = hitStopLv1;
        HitDetect.potentialKnockBack = new Vector2(1.5f, 0);
        HitDetect.initialProration = .85f;
        HitDetect.attackLevel = 1;
        HitDetect.guard = "Low";

        HitDetect.allowMedium = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
    }

    void CrouchingMHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit1.offset = new Vector2(.68f, -.78f);
        hit1.size = new Vector2(.92f, .087f);
        HitDetect.damage = 40;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv1;
        HitDetect.potentialHitStop = hitStopLv1;
        HitDetect.potentialKnockBack = new Vector2(1.3f, 0);
        HitDetect.initialProration = .75f;
        HitDetect.attackLevel = 1;
        HitDetect.guard = "Low";

        HitDetect.allowMedium = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
    }

    void JumpMHitBoxFirst()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit1.offset = new Vector2(.51f, -.12f);
        hit1.size = new Vector2(.73f, .26f);
        hit2.offset = new Vector2(1.02f, 0f);
        hit2.size = new Vector2(.295f, .32f);
        HitDetect.damage = 42;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv2;
        HitDetect.potentialHitStop = hitStopLv2;
        HitDetect.potentialKnockBack = new Vector2(1.2f, 0);
        HitDetect.potentialAirKnockBack = new Vector2(1.2f, 2f);
        HitDetect.initialProration = .85f;
        HitDetect.attackLevel = 1;
        HitDetect.guard = "Overhead";

        HitDetect.horiSlash = true;

        HitDetect.allowLight = true;
        HitDetect.allowMedium = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
        HitDetect.jumpCancellable = true;
    }

    void JumpMHitBoxSecond()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit1.offset = new Vector2(-.44f, -.06f);
        hit1.size = new Vector2(.76f, .33f);

        HitDetect.damage = 44;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv3;
        HitDetect.potentialHitStop = hitStopLv2;
        HitDetect.potentialKnockBack = new Vector2(1.3f, 0);
        HitDetect.potentialAirKnockBack = new Vector2(1.2f, 2f);
        HitDetect.initialProration = .85f;
        HitDetect.attackLevel = 1;
        HitDetect.guard = "Overhead";

        HitDetect.horiSlash = true;

        HitDetect.allowLight = true;
        HitDetect.allowMedium = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
    }

    void StandingHHitBoxFirst()
    {
        ClearHitBox();

        hit1.enabled = true;
        hit1.offset = new Vector2(.75f, -.23f);
        hit1.size = new Vector2(.73f, .32f);
        HitDetect.damage = 30;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv3;
        HitDetect.potentialHitStop = hitStopLv1;
        HitDetect.potentialKnockBack = new Vector2(1f, 0);
        HitDetect.potentialAirKnockBack = new Vector2(1f, 1.5f);
        HitDetect.initialProration = .9f;
        HitDetect.attackLevel = 2;
        HitDetect.guard = "Mid";

        HitDetect.slash = true;
        HitDetect.allowLight = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
        HitDetect.jumpCancellable = true;
    }

    void StandingHHitBoxSecond()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit1.offset = new Vector2(.86f, .22f);
        hit1.size = new Vector2(.95f, .33f);
        HitDetect.damage = 42;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv3;
        HitDetect.potentialHitStop = hitStopLv2;
        HitDetect.potentialKnockBack = new Vector2(1.3f, 0);
        HitDetect.potentialAirKnockBack = new Vector2(1f, 1.5f);
        HitDetect.attackLevel = 2;
        HitDetect.guard = "Mid";

        HitDetect.slash = true;
        HitDetect.allowHeavy = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
        HitDetect.jumpCancellable = true;
    }

    void CrouchingHHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;
        hit1.offset = new Vector2(.9f, .82f);
        hit1.size = new Vector2(.51f, .775f);
        hit2.offset = new Vector2(1.1f, .38f);
        hit2.size = new Vector2(.5f, 1.1f);
        hit3.offset = new Vector2(.7f, -.22f);
        hit3.size = new Vector2(.92f, .95f);
        HitDetect.damage = 60;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv4;
        HitDetect.potentialHitStop = hitStopLv2;
        HitDetect.potentialKnockBack = new Vector2(.7f, 3.5f);
        HitDetect.potentialAirKnockBack = new Vector2(.3f, 3f);
        HitDetect.initialProration = .8f;
        if (HitDetect.OpponentDetector.Actions.standing)
            HitDetect.forcedProration = .8f;
        HitDetect.attackLevel = 3;
        HitDetect.guard = "Mid";

        HitDetect.launch = true;
        HitDetect.vertSlash = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
        HitDetect.jumpCancellable = true;
    }

    void JumpHHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;
        hit4.enabled = true;
        hit5.enabled = true;

        hit1.offset = new Vector2(.59f, -.09f);
        hit1.size = new Vector2(.52f, .26f);
        hit2.offset = new Vector2(.91f, .027f);
        hit2.size = new Vector2(.27f, .26f);
        hit3.offset = new Vector2(.88f, .265f);
        hit3.size = new Vector2(.275f, .228f);
        hit4.offset = new Vector2(.76f, .41f);
        hit4.size = new Vector2(.22f, .08f);
        hit5.offset = new Vector2(.56f, .49f);
        hit5.size = new Vector2(.37f, .087f);

        HitDetect.damage = 50;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv4;
        HitDetect.potentialHitStop = 6;
        HitDetect.potentialKnockBack = new Vector2(1.2f, 0f);
        HitDetect.potentialAirKnockBack = new Vector2(.8f, 1.5f);
        HitDetect.attackLevel = 2;
        HitDetect.guard = "Overhead";

        HitDetect.horiSlash = true;

        HitDetect.allowHeavy = true;
        HitDetect.allowBreak = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
        HitDetect.jumpCancellable = true;
    }

    void BreakCharge()
    {
        sinCharge++;
        HitDetect.anim.SetInteger("SinCharge", sinCharge);
    }

    void FullBreakCharge()
    {
        sinCharge = 5;
        HitDetect.anim.SetInteger("SinCharge", sinCharge);
    }

    void ResetCharge()
    {
        sinCharge = 0;
        HitDetect.anim.SetInteger("SinCharge", sinCharge);
    }

    void StandingBHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;
        hit4.enabled = true;
        hit5.enabled = true;
        hit6.enabled = true;
        hit7.enabled = true;

        hit1.offset = new Vector2(.64f, -.61f);
        hit1.size = new Vector2(.67f, .56f);
        hit2.offset = new Vector2(.81f, -.09f);
        hit2.size = new Vector2(.55f, .6f);
        hit3.offset = new Vector2(.7f, .33f);
        hit3.size = new Vector2(.62f, .25f);
        hit4.offset = new Vector2(.55f, .54f);
        hit4.size = new Vector2(.57f, .21f);
        hit5.offset = new Vector2(0f, .7f);
        hit5.size = new Vector2(1f, .3f);
        hit6.offset = new Vector2(-.55f, .57f);
        hit6.size = new Vector2(.22f, .22f);
        hit7.offset = new Vector2(-.7f, .45f);
        hit7.size = new Vector2(.1f, .12f);

        HitDetect.damage = 85 + (10 * sinCharge);
        if (sinCharge - 1 > 0)
            HitDetect.armorDamage = sinCharge - 1;
        else
            HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialKnockBack = new Vector2(1.5f, 0f);
        HitDetect.potentialAirKnockBack = new Vector2(1.5f, 2f);
        HitDetect.potentialHitStun = hitStunLv3 + sinCharge;
        HitDetect.potentialHitStop = hitStopLv3 + sinCharge;
        HitDetect.initialProration = 1;
        HitDetect.attackLevel = 3;
        HitDetect.guard = "Mid";

        HitDetect.vertSlash = true;
        if (sinCharge > 1)
            HitDetect.forceCrouch = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
        HitDetect.jumpCancellable = true;

        sinCharge = 0;
        HitDetect.anim.SetInteger("SinCharge", sinCharge);
    }

    void CrouchBHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;

        hit1.offset = new Vector2(.91f, -.72f);
        hit1.size = new Vector2(1f, .43f);

        HitDetect.damage = 80 + (10 * sinCharge);
        if (sinCharge - 1 > 0)
            HitDetect.armorDamage = sinCharge - 1;
        else
            HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialKnockBack = new Vector2(.5f, 1.5f);
        HitDetect.potentialHitStun = 60;
        HitDetect.potentialHitStop = hitStopLv2 + sinCharge;
        HitDetect.initialProration = 1;
        HitDetect.attackLevel = 3;
        HitDetect.guard = "Low";

        HitDetect.sweep = true;
        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;

        sinCharge = 0;
        HitDetect.anim.SetInteger("SinCharge", sinCharge);
    }

    void JumpBHitBox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;
        hit4.enabled = true;
        hit5.enabled = true;

        hit1.offset = new Vector2(-.1f, -.21f);
        hit1.size = new Vector2(.71f, .74f);
        hit2.offset = new Vector2(.26f, .09f);
        hit2.size = new Vector2(.57f, .73f);
        hit3.offset = new Vector2(.47f, .32f);
        hit3.size = new Vector2(.51f, .74f);
        hit4.offset = new Vector2(.61f, .665f);
        hit4.size = new Vector2(.395f, 1.16f);
        hit5.offset = new Vector2(.82f, .71f);
        hit5.size = new Vector2(.21f, .76f);

        HitDetect.damage = 85;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialKnockBack = new Vector2(2f, 0f);
        HitDetect.potentialAirKnockBack = new Vector2(2f, 2f);
        HitDetect.potentialHitStun = 25;
        HitDetect.potentialHitStop = hitStopLv4;
        HitDetect.initialProration = .9f;
        HitDetect.attackLevel = 4;
        HitDetect.guard = "Overhead";

        HitDetect.allowSpecial = true;
        HitDetect.allowSuper = true;
    }

    void ThrowInit()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;


        hit1.offset = new Vector2(.29f, -.09f);
        hit1.size = new Vector2(.28f, .9f);

        HitDetect.armorDamage = 1;
        HitDetect.durabilityDamage = 50;
        HitDetect.potentialHitStun = 60;
        HitDetect.potentialHitStop = 20;
        HitDetect.attackLevel = 0;
        HitDetect.guard = "Unblockable";

        HitDetect.crumple = true;
        HitDetect.grab = true;

    }

    void ThrowSecondHit()
    {
        ClearHitBox();
        hit1.enabled = true;


        hit1.offset = new Vector2(.975f, .1f);
        hit1.size = new Vector2(.45f, .79f);

        HitDetect.potentialHitStun = 60;
        HitDetect.potentialHitStop = 0;
        HitDetect.attackLevel = 0;
        HitDetect.guard = "Unblockable";

        HitDetect.launch = true;
        HitDetect.grab = true;

    }

    void ThrowThirdHit()
    {
        ClearHitBox();
        hit1.enabled = true;


        hit1.offset = new Vector2(1.4f, .5f);
        hit1.size = new Vector2(1.3f, .47f);
        HitDetect.damage = 100;
        HitDetect.initialProration = .7f;
        HitDetect.forcedProration = .7f;
        HitDetect.potentialAirKnockBack = new Vector2(2.5f, 2f);
        HitDetect.potentialHitStun = 60;
        HitDetect.potentialHitStop = 7;
        HitDetect.attackLevel = 4;
        HitDetect.guard = "Unblockable";

        HitDetect.piercing = true;
        HitDetect.allowWallBounce = true;
        HitDetect.allowSuper = true;
    }

    void BBCycleHit3()
    {
        ClearHitBox();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;

        hit1.offset = new Vector2(.54f, -.26f);
        hit1.size = new Vector2(.7f, .94f);
        hit2.offset = new Vector2(-.22f, .8f);
        hit2.size = new Vector2(.44f, .74f);
        hit3.offset = new Vector2(-.58f, .71f);
        hit3.size = new Vector2(.41f, .63f);

        HitDetect.damage = 15;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 50;
        HitDetect.potentialKnockBack = new Vector2(.5f, .5f);
        HitDetect.potentialAirKnockBack = new Vector2(.5f, -.75f);
        HitDetect.potentialHitStun = hitStunLv2;
        HitDetect.potentialHitStop = 1;
        HitDetect.initialProration = .7f;
        HitDetect.attackLevel = 2;
        HitDetect.guard = "Overhead";
        HitDetect.slash = true;

        HitDetect.allowGroundBounce = true;

        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void BBCycleHit5()
    {
        ClearHitBox();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;

        hit1.offset = new Vector2(.6f, .01f);
        hit1.size = new Vector2(.64f, .96f);
        hit2.offset = new Vector2(-.58f, .4f);
        hit2.size = new Vector2(.82f, .5f);
        hit3.offset = new Vector2(-.85f, -.1f);
        hit3.size = new Vector2(.48f, .62f);

        HitDetect.damage = 15;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 50;
        HitDetect.potentialKnockBack = new Vector2(.5f, .5f);
        HitDetect.potentialAirKnockBack = new Vector2(.5f, -.75f);
        HitDetect.potentialHitStun = hitStunLv2;
        HitDetect.potentialHitStop = 1;
        HitDetect.initialProration = .7f;
        HitDetect.attackLevel = 2;
        HitDetect.guard = "Overhead";
        HitDetect.slash = true;

        HitDetect.allowGroundBounce = true;

        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void BBFinalHit()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;

        hit1.offset = new Vector2(.95f, -.375f);
        hit1.size = new Vector2(.81f, .8f);
        hit2.offset = new Vector2(.93f, .26f);
        hit2.size = new Vector2(.41f, .5f);
        hit3.offset = new Vector2(.45f, .59f);
        hit3.size = new Vector2(.63f, .24f);

        HitDetect.damage = 110;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 0;
        HitDetect.potentialKnockBack = new Vector2(1.5f, 3.5f);
        HitDetect.potentialAirKnockBack = new Vector2(2f, -2f);
        HitDetect.potentialHitStun = 30;
        HitDetect.potentialHitStop = hitStopLv4;
        HitDetect.initialProration = .8f;
        HitDetect.attackLevel = 4;
        HitDetect.guard = "Overhead";

        HitDetect.shatter = true;
        if (HitDetect.OpponentDetector.Actions.standing)
            HitDetect.launch = true;
        HitDetect.allowGroundBounce = true;

        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void HRCycleHit()
    {
        ClearHitBox();

        hit1.enabled = true;

        hit1.offset = new Vector2(.65f, -.55f);
        hit1.size = new Vector2(.38f, .55f);

        HitDetect.damage = 22;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialKnockBack = new Vector2(1f, 0f);
        HitDetect.potentialAirKnockBack = new Vector2(2f, 1.5f);
        HitDetect.forcedProration = 1.1f;
        HitDetect.potentialHitStun = 24;
        HitDetect.potentialHitStop = 1;
        HitDetect.attackLevel = 3;
        HitDetect.guard = "Low";

        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void HRLaunchHit()
    {
        ClearHitBox();

        hit1.enabled = true;

        hit1.offset = new Vector2(.65f, -.55f);
        hit1.size = new Vector2(.38f, .55f);

        HitDetect.damage = 0;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialKnockBack = new Vector2(0f, 2f);
        HitDetect.potentialAirKnockBack = new Vector2(2f, 2f);
        HitDetect.potentialHitStun = 24;
        HitDetect.potentialHitStop = 0;
        HitDetect.attackLevel = 3;
        HitDetect.guard = "Low";

        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void HRSlashHit()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;
        hit4.enabled = true;
        hit5.enabled = true;

        hit1.offset = new Vector2(.59f, -.09f);
        hit1.size = new Vector2(.52f, .26f);
        hit2.offset = new Vector2(.91f, .027f);
        hit2.size = new Vector2(.27f, .26f);
        hit3.offset = new Vector2(.88f, .265f);
        hit3.size = new Vector2(.275f, .228f);
        hit4.offset = new Vector2(.76f, .41f);
        hit4.size = new Vector2(.22f, .08f);
        hit5.offset = new Vector2(.56f, .49f);
        hit5.size = new Vector2(.37f, .087f);

        HitDetect.damage = 50;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 100;
        HitDetect.potentialHitStun = hitStunLv4;
        HitDetect.potentialHitStop = hitStopLv1;
        HitDetect.potentialKnockBack = new Vector2(1.2f, 3f);
        HitDetect.potentialAirKnockBack = new Vector2(.8f, 3f);
        HitDetect.attackLevel = 3;
        HitDetect.guard = "Mid";

        HitDetect.slash = true;
        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void HRUpperHit1()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;

        hit1.offset = new Vector2(.66f, .4f);
        hit1.size = new Vector2(.36f, .74f);
        hit2.offset = new Vector2(.57f, -.08f);
        hit2.size = new Vector2(.25f, .76f);

        HitDetect.damage = 80;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 0;
        HitDetect.potentialHitStun = hitStunLv4;
        HitDetect.potentialHitStop = hitStopLv4;
        HitDetect.potentialKnockBack = new Vector2(1.2f, 3f);
        HitDetect.attackLevel = 5;
        HitDetect.guard = "Mid";

        HitDetect.shatter = true;
        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void HRUpperHit2()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;

        hit1.offset = new Vector2(.4f, .87f);
        hit1.size = new Vector2(.82f, .75f);
        hit2.offset = new Vector2(.75f, .51f);
        hit2.size = new Vector2(.61f, .69f);
        hit3.offset = new Vector2(.79f, -.02f);
        hit3.size = new Vector2(.28f, .56f);

        HitDetect.damage = 120;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 0;
        HitDetect.potentialHitStun = 100;
        HitDetect.potentialHitStop = 15;
        HitDetect.potentialKnockBack = new Vector2(1.5f, 5.5f);
        HitDetect.attackLevel = 5;
        HitDetect.guard = "Mid";

        HitDetect.shatter = true;
        HitDetect.launch = true;
        HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void BCWeakCharge()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;

        hit1.offset = new Vector2(1f, .11f);
        hit1.size = new Vector2(.84f, .8f);
        hit2.offset = new Vector2(.59f, .24f);
        hit2.size = new Vector2(.93f, .28f);

        HitDetect.damage = 85;
        HitDetect.armorDamage = 1;
        HitDetect.durabilityDamage = 0;
        HitDetect.potentialHitStun = 48;
        HitDetect.potentialHitStop = hitStopLv4;
        HitDetect.potentialKnockBack = new Vector2(3f, 2f);
        HitDetect.attackLevel = 3;
        HitDetect.guard = "Mid";

        //HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;
    }

    void BCFullCharge()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;

        hit1.offset = new Vector2(1.14f, 018f);
        hit1.size = new Vector2(1f, 1f);
        hit2.offset = new Vector2(.94f, .24f);
        hit2.size = new Vector2(1.6f, .28f);
        hit3.offset = new Vector2(.97f, .5f);
        hit3.size = new Vector2(.64f, .61f);

        HitDetect.damage = 140;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 0;
        HitDetect.potentialHitStun = 60;
        HitDetect.potentialHitStop = hitStopLv4;
        HitDetect.potentialKnockBack = new Vector2(4f, 2.5f);
        HitDetect.attackLevel = 5;
        HitDetect.guard = "Mid";

        HitDetect.allowWallStick = true;
        HitDetect.shatter = true;
        //HitDetect.usingSpecial = true;
        HitDetect.allowSuper = true;

        sinCharge = 0;
        HitDetect.anim.SetInteger("SinCharge", sinCharge);
    }

    void JSabreHitbox()
    {
        ClearHitBox();
        HitDetect.Actions.AttackActive();

        hit1.enabled = true;
        hit2.enabled = true;
        hit3.enabled = true;

        hit1.offset = new Vector2(.145f, 1.4f);
        hit1.size = new Vector2(.57f, 1.46f);
        hit2.offset = new Vector2(.58f, .56f);
        hit2.size = new Vector2(.33f, 2.6f);
        hit3.offset = new Vector2(.91f, .63f);
        hit3.size = new Vector2(.375f, 1.8f);

        HitDetect.damage = 300;
        HitDetect.armorDamage = 0;
        HitDetect.durabilityDamage = 0;
        HitDetect.potentialHitStun = 600;
        HitDetect.potentialHitStop = 24;
        HitDetect.potentialKnockBack = new Vector2(.7f, 8f);
        HitDetect.initialProration = .8f;
        HitDetect.forcedProration = .8f;
        HitDetect.attackLevel = 10;
        HitDetect.guard = "Mid";

        HitDetect.vertSlash = true;

        HitDetect.launch = true;
        HitDetect.shatter = true;
        HitDetect.usingSuper = true;
    }
}