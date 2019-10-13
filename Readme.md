### Idea-Date API Documentation 

---


Account Creation 

---

```/user/create```

Functional?: ✅ <br>
Type: POST <br>
Consumes: <br>
JSON Object with 
- Name (String)
- Role (String), one of: 'designer', 'developer', 'project manager'
- GitHub Username (String)
- Email Address (String)
- Description (String)
- Tech Stack (JSON Array of String)

e.g. call create endpoint for a new user
```{"Name": "Will", "Role": "developer", "GitHub": "wjwalcher", "Email": "wjwalcher@gmail.com", "Description": "A really cool dude 😎", "TechStack": ["c#", "java", "python", "c"]}```

Produces: 
- UID to store in cookie for quick 'authentication' purposes

```/api/user/edit```

Functional?: ❌ <br>
Consumes: <br>
JSON Object containing fields of user to edit/update
- Name (String)
- Role (String), one of: 'designer', 'developer', 'project manager'
- GitHub Username (String)
- Description (String)
- Tech Stack (List of String)
Any of these that are not empty will get updated

e.g. ```{"Name": "new_name", "Role": "", "GitHub": "", "Description": "", "TechStack": "", "UID": ""}```
will update only the name of the user 

Produces:
- On success, return code 200 OK 
- On failure, return code 403 UNAUTHORIZED

```/api/user/delete```

Functional?: ❌ <br>
Consumes: <br>
(Hacky and not smart) a 'user id' to delete <br>

Produces:
- On success, return code 200 OK
- On failure, return code 403 UNAUTHORIZED


Project Management View 

---

```/api/project/create```

Functional?: ✅ <br>
Consumes: <br>
JSON Object containing
- Project name (String)
- GitHub URL (String)
- Project description (String)
- Tech Stack (List of String)

Produces:
- ```project_uid``` which should be saved client-side in a cookie to be passed to project-related API calls

e.g. ```{"Name": "Super Cool Project", "GitHubURL": "https://www.github.com/username/someproject", "Description": "Test Description", "Founder": "Insert Founder's UID here when they create project"}```


```/api/project/edit```

Functional?: ❌ <br>
Consumes: <br>
JSON Object containing the fields of edits to be made 
- Project name (String) 
- GitHub URL (String)
- Project description (String)
- Tech Stack (List of String)
Any of these that are not empty will get updated

Produces:
- On success, return code 200 OK
- On failure, return code 403 UNAUTHORIZED


```/api/project/delete```

Functional?: ❌ <br>
Consumes: <br>
(Again, not smart) Project UID (String) <br>

Produces:
- On success, return code 200 OK
- On failure, return code 403 UNAUTHORIZED


Collaborator View 

---

```/api/collaborator/getprojects/{uid}```

Functional?: ✅ <br>
Get the next project for a given user. The backend tracks which projects a user has seen so far, and will deliver a new project each time this is called. <br>
Type: GET <br>
Consumes: 
- Collaborator's UID 

Produces: <br>
(On success - status code 200 OK) 
- A JSON response containing all the attributes of a project (as specified above)
- This will also include the unique ID of the project (which you will pass to likeProject and dismissProject) 

(On failure, return status code 403 UNAUTHORIZED)


```/api/collaborator/likeProject```

Functional?: ✅ <br>
Type: POST <br>
Consumes: <br>
JSON object containing collaborator's UID and project's UID <br>

Effects: 
- Adds user ID of user to project's like list
- Adds project ID to user's seen list

e.g.
```{"User": "2c5f5ccc36fb43f2a38c34af29ff5485", "Project": "f64dab1cae5c4dbb9963254bac298510"}```

```/api/collaborator/dismissProject```

Functional?: ✅ <br>
Type: POST <br>
Consumes: <br>
JSON object containing collaborator's UID and project's UID <br>

Effects: 
- Adds project ID to user's seen list

e.g. 
```{"User": "2c5f5ccc36fb43f2a38c34af29ff5485", "Project": "f64dab1cae5c4dbb9963254bac298510"}```


Recruiting View

---

```/api/recruiter/{projectId}```

Gets all the users (and their info) who liked this project <br>
Functional?: ✅ <br>
Returns a list of the users (and user IDs) of the users that liked the given project <br>
Type: GET <br>
Consumes: 
- ProjectId (path variable) of the project to get the likes for

Produces: 
- On success (status code 200 OK), list of JSON objects containing usernames, GitHub profiles, and UIDs of each user

e.g. 
```[
    {
        "uid": "2c5f5ccc36fb43f2a38c34af29ff5485",
        "name": "Will",
        "role": "developer",
        "gitHub": "wjwalcher",
        "description": "A really cool guy",
        "email": null,
        "techStack": [
            "java",
            "c#",
            "python"
        ],
        "likedProjects": [
            "f64dab1cae5c4dbb9963254bac298510",
            "bea9d42d1057409dbaa1bd2fa8380e23"
        ],
        "dismissedProjects": []
    }
]
```


```/api/recruiter/likeuser```

Functional?: ✅ <br>
Type: POST <br>
Consumes:
- User ID of the user to like

Effects: 
- Sends an email with info to the collaborator

e.g. ```{"project_uid": "bea9d42d1057409dbaa1bd2fa8380e23","user_uid": "e97a9f7bafbc42b9b1a3adf982c68a8f"}```

```/api/recruiter/dismissUser```

Functional?: ✅ <br>
Type: POST <br>
Consumes:
- User ID of the user to dismiss

Effects: 
- Remove user from project list

e.g. ```{"project_uid": "bea9d42d1057409dbaa1bd2fa8380e23","user_uid": "e97a9f7bafbc42b9b1a3adf982c68a8f"}```

