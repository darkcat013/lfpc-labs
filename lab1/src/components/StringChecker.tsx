import { Button, Grid, TextField } from "@mui/material"
import React, { useEffect, useRef, useState } from "react"
import { checkString, checkStringAutomata } from "../functions/StringCheck";
import { Relation} from "../interfaces"

interface StringCheck {
	map: Map<string, string[]>
	relations: Relation[]
}

const StringChecker: React.FC<StringCheck> = ({ map, relations }) => {

	const textField = useRef<any>(null);
	const [startingChars, setStartingChars] = useState<string[] | undefined>();
	const [endingChars, setEndingChars] = useState<string[] | undefined>();

	useEffect(() => {
		const tempStartChars = map.get("S")?.map(x => x[0]);
		setStartingChars(tempStartChars);

		let tempEndChars: string[] | undefined = [];
		for (let key of map.keys()) {
			let endChars: string[] | undefined = map.get(key)?.filter(x => x.length === 1);
			if (endChars !== undefined)
				tempEndChars.push(...endChars)
		}
		setEndingChars(tempEndChars);

	}, [map])

	const handleClick = () => {
		console.log(relations);
		console.log(map);
		let str = textField.current.value;
		if (str.length < 2) {
			alert("string is too short")
		}
		else if (!startingChars?.includes(str[0])) {
			alert("Wrong starting character")
		}
		else if (!endingChars?.includes(str[str.length - 1])) {
			alert("Wrong terminal character")
		}
		else if (checkStringAutomata(relations, str)) {
			alert("String is correct")
		}
	}
	return (
		<Grid container direction="column">
			<Grid item>
				<TextField size="small" inputRef={textField} />
			</Grid>
			<Grid item sx={{ mt: 2 }}>
				<Button color="primary" variant="contained" onClick={handleClick}> Check string</Button>
			</Grid>
		</Grid>
	)
}

export default StringChecker