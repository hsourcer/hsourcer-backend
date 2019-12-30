import pytest
import requests


@pytest.fixture
def module_url(url):
  return "{}/Team".format(url)

def test_teams_get(module_url, login_url, username, password, config):

  with requests.Session() as s:
    payload = {
      "email": username,
      "password": password
    }
    r = s.post(login_url, json=payload)
    assert(r.status_code==200)

    s.headers.update({
      "Authorization": "Bearer {}".format(r.json().get('userToken', ''))
      })
    url = '{}/{}'.format(module_url, '')

    r = s.get(url)

    data = r.json()
    
    assert(isinstance(data, list))
    assert(r.status_code==200)

    if len(data) > 0:
        team = data[0]

        for user in team.get('users', []):
            for k, v in user.items():
                assert( config["USER_MODEL"].get(k) == type(v).__name__ )

