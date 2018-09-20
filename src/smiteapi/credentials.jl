import YAML

struct Credentials
    devid::Int
    authkey::String
end

function loadcredentials(filename = ".auth.yaml")::Credentials
    data = YAML.load(open(filename))
    Credentials(data["devid"], data["authkey"])
end
