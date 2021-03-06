import pytest
import requests

from ..conftest import randstring

@pytest.fixture
def module_url(url):
    return "{}/Auth".format(url)

def test_login_get(module_url):
    url = '{}/{}'.format(module_url, 'login')
    r = requests.get(url)
    assert(r.status_code==405)


def test_login_post_ok(module_url, username, password, user_model):
    url = '{}/{}'.format(module_url, 'login')
    payload = {
        "email": username,
        "password": password
    }

    r = requests.post(url, json=payload)

    assert(r.status_code==200)

    response = r.json()
    for k, v in user_model.items():
        assert(k in response)
        assert(type(response[k] == v))

  
def test_login_post_error(module_url, username, user_model):
    url = '{}/{}'.format(module_url, 'login')
    payload = {
    "email": username,
    "password": randstring(10)
    }

    r = requests.post(url, json=payload)
    assert(r.status_code==404)

    r = requests.post(url, data=payload)
    assert(r.status_code==415)

    # r = requests.post(url, json={})
    # assert(r.status_code==400)

    r = requests.post(url, json={"email": "", "password": ""})
    assert(r.status_code==404)

    r = requests.post(url, json={"email": ""})
    assert(r.status_code==404)

    # r = requests.post(url, json={"password": ""})
    # assert(r.status_code==404)

def test_logout(url, module_url, username, password):
    with requests.Session() as s:
        _url = '{}/{}'.format(module_url, 'login')
        payload = {
            "email": username,
            "password": password
        }
        r = s.post(_url, json=payload)
        assert(r.status_code==200)

        s.headers.update({
            "Authorization": "Bearer {}".format(r.json().get('userToken', ''))
        })
        _url = '{}/{}'.format(module_url, 'logout')
        r = s.post(_url)
        assert(r.status_code==200)
        # Double logout
        r = s.post(_url)
        assert(r.status_code==200)
        # Accessing restricted endpoint after logout
        _url = '{}/{}'.format(url, 'Team')
        r = s.get(_url)
        assert(r.status_code == 401)