@echo off 
git checkout master
git add .
git status
set /p message=Enter commit messages: 
git commit -am "%message%"
git push git@github.com:muthuganesh/GiftEntertainmentTracker.git
pause