function write(datafile, data)
    f = open(datafile, "w")
    JSON.print(f, data)
    close(f)
end
