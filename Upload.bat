@echo off 
git checkout master
git add .
git status
set /p message=Enter commit messages: 
git commit -am "%message%"
git remote add origin git@github.com:muthuganesh/GiftEntertainmentTracker.git
git push
pause