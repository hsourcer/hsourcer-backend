import pytest
import requests

from ..conftest import randstring

@pytest.fixture
def module_url(url):
  return "{}/Account".format(url)

def test_account(module_url, login_url, admin_username, password, team_id):
    """
    Create, edit and deactivate user.

    - team_id is passed from config and points to test team
    - user can only be deactivated, not deleted. Duplicate e-mail addresses
    won't be allowed even if the user is deactivated
    """
    with requests.Session() as s:
        payload = {
            "email": admin_username,
            "password": password
        }
        r = s.post(login_url, json=payload)
        assert(r.status_code==200)
        s.headers.update({
            "Authorization": "Bearer {}".format(r.json().get('userToken', ''))
        })


        name = randstring(5)
        data = {
            "teamId": team_id,
            "firstName": name,
            "lastName": name,
            "position": "string",
            "phoneNumber": "123",
            "email": "{}@hscr.site".format(name),
            "userRole": 1
        }

        # Creating user
        url = '{}/{}'.format(module_url, 'create')
        r = s.post(url, json=data)
        assert(r.status_code==201)
        user_id = r.json()

        # Invalid user
        # Duplicated user
        r = s.post(url, json=data)
        assert(r.status_code==500)
        # Invalid team ID
        invalid_data = data.copy()
        invalid_data["teamId"] = 0
        r = s.post(url, json=invalid_data)
        assert(r.status_code==500)
        # Invalid e-mail
        invalid_data = data.copy()
        invalid_data["email"] = "@hscr.site"
        r = s.post(url, json=invalid_data)
        assert(r.status_code==500)
        # Invalid user role
        invalid_data = data.copy()
        invalid_data["userRole"] = 999999999
        r = s.post(url, json=invalid_data)
        assert(r.status_code==500)
        # Invalid user role
        invalid_data = data.copy()
        invalid_data["userRole"] = -1
        r = s.post(url, json=invalid_data)
        assert(r.status_code==500)
        # Missing keys
        invalid_data = data.copy()
        invalid_data.pop("firstName")
        r = s.post(url, json=invalid_data)
        assert(r.status_code==500)


        # Editing user
        url = '{}/{}'.format(module_url, 'update')
        new_name = randstring(5)
        data = {
            "userId": user_id,
            # "active": True,
            "firstName": new_name,
            "lastName": new_name,
            "position": "string",
            "phoneNumber": "string",
            "email": "{}@hscr.site".format(new_name)
        }
        r = s.post(url, json=data)
        assert(r.status_code==200)

        # Nonexistent user ID
        invalid_data = data.copy()
        invalid_data['userId'] = -1
        r = s.post(url, json=invalid_data)
        assert(r.status_code==404)
        invalid_data['userId'] = 99999
        r = s.post(url, json=invalid_data)
        assert(r.status_code==404)
        # Over Int32
        invalid_data['userId'] = 99999999999
        r = s.post(url, json=invalid_data)
        assert(r.status_code==400)


        # Deactivating user
        url = '{}/{}'.format(module_url, 'delete')
        data = {"userId": user_id}
        r = s.post(url, json=data)
        assert(r.status_code==200)

        # Wrong user ID
        r = s.post(url, json={"userId": -1})
        assert(r.status_code==404)
        # Missing user ID
        r = s.post(url, json={})
        assert(r.status_code==404)
        # User ID as string
        r = s.post(url, json={"userId": "5"})
        assert(r.status_code==200)