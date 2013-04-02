@echo off 
git checkout master
git add .
git status
set /p message=Enter commit messages: 
git commit -am "%message%"
git push add origin
pause