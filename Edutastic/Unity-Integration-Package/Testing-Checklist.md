# ✅ Testing Checklist

## Before Testing

- [ ] API server is running (`dotnet run`)
- [ ] API URL is set to host computer's IP (not localhost)
- [ ] Firewall allows port 5069
- [ ] APIClient GameObject exists in Unity scene
- [ ] Both devices on same network (WiFi)

## Test 1: Connection

- [ ] Press Play in Unity
- [ ] Check Console for `[API]` logs
- [ ] No connection errors

## Test 2: Load Questions

- [ ] Call `QuestionLoader.LoadQuestions(moduleId)`
- [ ] Console shows: `Loaded X questions`
- [ ] Questions appear in game

## Test 3: Start Game Session

- [ ] Call `GameSession.Start(userId, moduleId)`
- [ ] Console shows: `Started: {session-id}`
- [ ] SessionId is not null

## Test 4: Submit Answer

- [ ] Call `GameSession.SubmitAnswer(questionId, answer, time)`
- [ ] Console shows: `Answer: true/false`
- [ ] Answer saved in database (check Supabase)

## Test 5: End Game

- [ ] Call `GameSession.End()`
- [ ] Console shows: `Ended! Score: X, XP: Y`
- [ ] Score saved in database
- [ ] User XP updated in profiles table

## Verification in Supabase

Go to Supabase Dashboard → Table Editor:

- [ ] `games` table has new session
- [ ] `game_answers` table has submitted answers
- [ ] `profiles` table shows updated `total_xp`

## Troubleshooting

If tests fail:

1. Check API terminal for errors
2. Check Unity Console for `[API]` error messages
3. Verify IP address is correct
4. Try pinging host computer: `ping 192.168.1.100`
5. Check both devices on same network
