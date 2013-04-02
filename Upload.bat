@echo off 
git checkout master
git add .
git status
set /p message=Enter commit messages: 
git commit -am "%message%"
git remote add origin git@github.com:muthuganesh/d.git
git push -u origin master
pause