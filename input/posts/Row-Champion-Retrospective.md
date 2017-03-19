Published: 2017-03-17
Title: Row Champion Retrospective
Lead: Row Champion is finally complete, so it's time to do some self-examination
Author: Bartosz
---

It supposed to be a short, simple project. That was what I've planned, especially after my previous this-takes-so-long-that-I-already-hate-it 
adventure with [Silly Ghosts](https://www.microsoft.com/en-us/store/p/silly-ghosts/9nblggh0913g). 
This time I had a plan and the idea seemed really good: quick turns and clear results. 
Check. Touch control and quick and clear feedback. Check. Simple, one screen game that I can do in my free time and won't take two years. Check.
A small number of assets to create. Check. Nothing can go wrong. Check?

### The idea or meh idea?

Someone told me it's not the idea that matter but the execution. I wish I've listened.
I think my main problem was, being blinded by the 'greatness' of my idea, I dismissed problems with execution,
thinking that the idea/gameplay will somehow defend itself. When I presented my first prototype to my
friends I could see that they had problems with getting the hang of it - some of them tried to swipe really fast and some of them had problems with timing, but I dismissed it,
thinking that all they need is a tutorial.

### Your own experience

The thing is, I didn't take my experience and knowledge about the game into the account. I knew how far the paddle should swing, I knew when to start the swing and 
I knew how to do int in the most efficient way. Everything about touch control was clear to me as day and night. And only for me. 
I've created the prototype, but I didn't use it as I should - to observe how the players play your game.

### Conveying the meaning

I didn't really put myself in the player's shoes and I didn't think how my 'message' would be understood. 
One great example here is the boost feature. My first idea was to reward the player for keeping the rhythm/consistent timing - 
to give the player bigger boost every time he starts a swing at the same time as the last one. 
So I've added a fire icon that would indicate the best moment to swing and change place if needed. The players asked why is a torch there 
(who would think that fire icon + red rectangle on meter looked like one) and why does it jump all over the place. I haven't managed to
convey the meaning at all.

### Bad game? Add features!

So what do you do when your game is failing? You add features. Multiplayer for example. I know right, the best idea ever, 
peoples love multiplayers. "I bet everybody will play my game when they see it has multiplayer", I thought. 
I forgot about a tiny detail, though - my code wasn't ready for multiplayer. Yeah, I could've probably hacked it together (and I should have, looking back on it), 
but I've decided that it's time for a rewrite, it was a bad code from the prototype anyway. So I extended my schedule and started coding again. 
Yep, threw my tried and tested, bug-free code to the trash and have written it again from scratch. 
It took me around 25 hours. Twenty-five hours! And then some more, fixing bugs and adjusting it again. I could finish a small game in that time, or do a game jam at least.

So after toiling away for the next 6 months of evenings, I showed everybody my game again, waiting for the world to be amazed. Well, let say the world was not that amazed.
Now, don't get me wrong. Multiplayer can be a great feature. It's also fun to code - I've learned a lot, really. But it's a lot of work for a one-man team. 
Another thing that I didn't foresee is multiplayer cost. You have to set up, test and then support the server. This takes a lot of time, especially if you're
learning it as you go. It isn't exactly free either.

### Fixing the design

I was knee deep in my game now, a lot of time was invested and I couldn't just walk away. So, with the help of r/gamedev and feedback Friday, 
I've started fixing the controls and UI. It took time because every time I've changed the core gameplay element, I had to adjust multiplayer, graphics and UI. 
Over a few months, I went through many iterations like that. I can't image how much time I've lost reworking the same stuff over and over again. 
Finally, after toiling away some more, I've started to get some positive reactions. 
I arrived at the very important moment, where people said - "that's a good game" or "that's a bad game", instead of "I don't know what's going on". 
Yeah, some people said it was boring or that it offered too little content for them, 
but those are great responses too. Not everybody will like your game, but they should dislike it because of a personal preference, not because they don't get it.

### Lessons

Overall, it was one hell of a ride. I hated working on it near the end and I wanted to just leave it and start working on something new.
I regret many things about this project, but at least I got a really great lesson out of it. A few of them actually:

* Create a prototype and use it properly (test ideas by yourself, but also test if others 'get' your game)
* Think about how your ideas/design elements might be interpreted by others
* Create a schedule and stick to it (I mean it!)
* Don't try to hide shortcomings by adding new features, fix them or scrap the project altogether
* Scrap the prototype code and write a new, clean one for the game
* Don't rewrite core code in the middle of project
* Multiplayer is a great feature, but resource heavy (time, money), not a good idea for an after work one-man project

Thank you for reading. I hope you've learned something from my experience. If you want to check it out, here you'll find the Android version of Row Champion. 

Have a great day!