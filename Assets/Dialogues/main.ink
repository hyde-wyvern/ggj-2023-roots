INCLUDE globals.ink
INCLUDE year_flows.ink
INCLUDE utilities.ink


You traveled to <>

~ temp rounded_current_year = INT(current_year)

{ rounded_current_year:
    - 1965:
        -> year_1965
    - 1966: 
        -> year_1966
    - 1967:
        -> year_1967
    - 1968:
        -> year_1968
    - 1969:
        -> year_1969
    - 1970:
        -> year_1970
    - 1971:
        -> year_1971
    - 1972: 
        -> year_1972
    - 1973:
        -> year_1973
    - 1974:
        -> year_1974
    - 1975:
        -> year_1975
    - 1976:
        -> year_1976
    - 1977:
        -> year_1977
    - 1978:
        -> year_1978
    - 1979:
        -> year_1979
    - 1980:
        -> year_1980
    - 1981:
        -> year_1981
    - 1982: 
        -> year_1982
    - 1983:
        -> year_1983
    - 1984:
        -> year_1984
    - 1985:
        -> year_1985
    - 1986:
        -> year_1986
    - 1987:
        -> year_1987
    - 1988:
        -> year_1988
    - 1989:
        -> year_1989
    - 1990:
        -> year_1990
    - 1991:
        -> year_1991
    - 1992:
        -> year_1992
    - 1993: 
        -> year_1993
    - 1994:
        -> year_1994
    - 1995:
        -> year_1995
    - 1996:
        -> year_1996
    - 1997:
        -> year_1997
    - 1998:
        -> year_1998
    - 1999:
        -> year_1999
    - 2000:
        -> year_2000
    - 2001:
        -> year_2001
    - 2002:
        -> year_2002
    - 2003:
        -> year_2003
    - 2004: 
        -> year_2004
    - 2005:
        -> year_2005
    - 2006:
        -> year_2006
    - 2007:
        -> year_2007
    - 2008:
        -> year_2008
    - 2009:
        -> year_2009
    - 2010:
        -> year_2010
    - 2011:
        -> year_2011
    - 2012:
        -> year_2012
    - 2013:
        -> year_2013
    - else:
        -> no_year
}
