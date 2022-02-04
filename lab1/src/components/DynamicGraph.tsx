import React, { useEffect, useState } from 'react';
import Graph from "react-vis-graph-wrapper";
import { Relations } from '../interfaces';
import "../styles.css"
const DynamicGraph: React.FC<Relations> = ({ relations }) => {

	const [nodes, setNodes] = useState<any[]>([]);
	const [edges, setEdges] = useState<any[]>([]);

	useEffect(() => {
		let uniqueKeys = [...new Set(relations.map(x => x.from))];

		let tempNodes = uniqueKeys.map(from => ({
			id: from,
			label: from,
			widthConstraint: {
				minimum: 30
			},
			borderWidth: 1
		}));
		tempNodes[tempNodes.length - 1].borderWidth = 4
		setNodes(tempNodes);

		let angle = Math.PI;
		let roundness = 0;
		let tempEdges = relations.map(x => ({
			from: x.from,
			to: x.to,
			label: x.edgeLabel,
			selfReference:
			{
				size: 20,
				angle: angle += 1
			},
			smooth: {
				enabled: true,
				type: "curvedCW",
				roundness: roundness +=0.1
			}
		}));

		setEdges(tempEdges);
	}, [relations]);

	const graph = {
		nodes: nodes,
		edges: edges
	};

	const options = {
		nodes: {
			shape: "circle",
			color: {
				background: "#FFFFFF",
				border: "#000000"
			}
		},
		edges: {
			color: "#000000"
		},
		height: "500px",
		width: "700px"
	};
	return (
		<Graph
			graph={graph}
			options={options}
			className="graph"
		/>
	);
}

export default DynamicGraph;