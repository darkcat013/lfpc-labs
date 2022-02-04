export const checkString = (map: Map<string, string[]>, str: string): boolean => {
	let start = map.get("S")?.filter(x => x[0] === str[0])[0][1];
	if (start !== undefined && check(map, start, 1, str)) {
		return true;
	}
	else return false;
}

const check = (map: Map<string, string[]>, next: string, pos: number, str: string): boolean | undefined => {
	let cont = map.get(next)?.filter(x => x[0] === str[pos])[0];
	
	if(cont === undefined) {
		alert("string incorrect at position " + (pos === str.length ? str.length : pos+1));
		return false;
	}
	else if(pos === str.length-1 && cont.length === 1) return true;
	
	if(check(map, cont[1], pos+1, str)) return true;
}