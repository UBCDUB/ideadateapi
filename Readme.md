### Idea-Date API Documentation 

---


Account Creation 

---

```/user/create```

Type: POST <br>
Consumes: <br>
JSON Object with 
- Name (String)
- Role (String), one of: 'designer', 'developer', 'project manager'
- GitHub Username (String)
- Description (String)
- Tech Stack (List of String)

e.g. call create endpoint for a new user
```{name: "Will", role: "developer", gh_username: "wjwalcher", desc: "A really cool dude 😎", "c#, java, python, c"}```

Produces: 
- On success, return code 200 OK (and a UID to store in cookie for quick 'authentication' purposes)
- On failure, return code 403 UNAUTHORIZED

```/user/edit```

Consumes: <br>
JSON Object containing fields of user to edit/update
- Name (String)
- Role (String), one of: 'designer', 'developer', 'project manager'
- GitHub Username (String)
- Description (String)
- Tech Stack (List of String)
Any of these that are not empty will get updated

e.g. ```{name: "new_name", role: "", gh_url: "", desc: "", tech_stack: "", uid: ""}```
will update only the name of the user 

Produces:
- On success, return code 200 OK 
- On failure, return code 403 UNAUTHORIZED

```/user/delete```

Consumes: <br>
(Hacky and not smart) a 'user id' to delete <br>

Produces:
- On success, return code 200 OK
- On failure, return code 403 UNAUTHORIZED


Project Management View 

---

```/project/create```

Consumes: <br>
JSON Object containing
- Project name (String)
- GitHub URL (String)
- Project description (String)
- Tech Stack (List of String)

Produces:
- On success, return code 200 OK (as well as a ```project_uid```)
- On failure, return code 403 UNAUTHORIZED


```/project/edit```

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


```/project/delete```

Consumes: <br>
(Again, not smart) Project UID (String) <br>

Produces:
- On success, return code 200 OK
- On failure, return code 403 UNAUTHORIZED


Collaborator View 

---

```/collaborator/getnextproject/{uid}```

Get the next project for a given user. The backend tracks which projects a user has seen so far, and will deliver a new project each time this is called. <br>
Type: GET <br>
Consumes: 
- Collaborator's UID 

Produces: <br>
(On success - status code 200 OK) 
- A JSON response containing all the attributes of a project (as specified above)
- This will also include the unique ID of the project (which you will pass to likeProject and dismissProject) 

(On failure, return status code 403 UNAUTHORIZED)


```/collaborator/likeProject```

Type: POST <br>
Consumes: <br>
JSON object containing collaborator's UID and project's UID <br>

Effects: 
- Adds user ID of user to project's like list
- Adds project ID to user's seen list

Produces:
- On success, return status code 200 OK
- On failure, return status code 403 UNAUTHORIZED

```/collaborator/dismissProject```

Type: POST <br>
Consumes: <br>
JSON object containing collaborator's UID and project's UID <br>

Effects: 
- Adds project ID to user's seen list

Produces:
- On success, return status code 200 OK
- On failure, return status code 403 UNAUTHORIZED


Recruiting View

---

```/recruiter/getlikes/{projectId}```

Returns a list of the users (and user IDs) of the users that liked the given project <br>
Type: GET <br>
Consumes: 
- ProjectId (path variable) of the project to get the likes for

Produces: 
- On success (status code 200 OK), list of JSON objects containing usernames, GitHub profiles, and UIDs of each user

```/recruiter/likeUser```

Type: POST <br>
Consumes:
- User ID of the user to like

Effects: 
- Sends a (notification? stretch)

Produces:
- On success, return status code 200 OK
- On failure, return status code 403 UNAUTHORIZED 

```/recruiter/dismissUser```
Type: POST <br>
Consumes:
- User ID of the user to dismiss

Effects: 
- Remove user from project list

Produces:
- On success, return status code 200 OK
- On failure, return status code 403 UNAUTHORIZED
